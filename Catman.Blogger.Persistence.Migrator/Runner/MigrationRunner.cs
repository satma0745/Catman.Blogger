namespace Catman.Blogger.Persistence.Migrator.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Catman.Blogger.Persistence.Migrator.Exceptions;
    using Catman.Blogger.Persistence.Migrator.Extensions;
    using Catman.Blogger.Persistence.Migrator.Migrations.Shared;
    using Dapper;

    internal class MigrationRunner
    {
        private readonly DbConnection _connection;

        public MigrationRunner(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task MigrateAsync(short? migrationIndex)
        {
            var possibleMigrations = GetMigrations();
            var appliedMigrations = await GetAppliedMigrationsAsync();

            EnsureConsistent(possibleMigrations, appliedMigrations, migrationIndex);

            var currentMigrationIndex = appliedMigrations.LastOrDefault()?.Index ?? -1;
            var desiredMigrationIndex = migrationIndex ?? possibleMigrations.Last().Info.Index;

            try
            {
                if (currentMigrationIndex < desiredMigrationIndex)
                {
                    var migrationsToApply = possibleMigrations
                        .Where(migration => migration.Info.Index > currentMigrationIndex &&
                                            migration.Info.Index <= desiredMigrationIndex);

                    await MigrateUpAsync(migrationsToApply);
                    Console.WriteLine("Migration completed successfully.");
                }
                else if (currentMigrationIndex > desiredMigrationIndex)
                {
                    var migrationsToUndo = possibleMigrations
                        .Where(migration => migration.Info.Index <= currentMigrationIndex &&
                                            migration.Info.Index > desiredMigrationIndex)
                        .OrderByDescending(migration => migration.Info.Index);

                    await MigrateDownAsync(migrationsToUndo);
                    Console.WriteLine("Migration completed successfully.");
                }
                else
                {
                    Console.WriteLine("Migration is not required.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Migration failed.");
            }
        }

        private static ICollection<(IMigrationInfo Info, IMigration Migration)> GetMigrations()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => !type.IsAbstract)
                .Where(type =>
                    (type.Implements<IMigration>(), type.HasAttribute<MigrationAttribute>()) switch
                    {
                        (true, false) => throw new MigrationAttributeRequiredException(type.Name),
                        (false, true) => throw new MigrationInterfaceImplementationRequiredException(type.Name),
                        (false, false) => false,
                        (true, true) => true
                    }
                )
                .Select(migrationType =>
                {
                    var attribute = migrationType.GetAttribute<MigrationAttribute>();
                    if (attribute.Index < 0)
                    {
                        throw new MigrationIndexMustBeNonNegativeException(attribute.Index);
                    }
                    
                    var migration = migrationType.Instantiate<IMigration>();

                    return (Info: (IMigrationInfo) attribute, Migration: migration);
                })
                .OrderBy(migration => migration.Info.Index)
                .ToList();
        }

        private async Task<ICollection<IMigrationInfo>> GetAppliedMigrationsAsync()
        {
            var historyExistsSql = @"SELECT EXISTS(
                                         SELECT 1
                                         FROM information_schema.tables
                                         WHERE table_name = 'migration_history'
                                     )";
            var historyExists = await _connection.ExecuteScalarAsync<bool>(historyExistsSql);

            if (!historyExists)
            {
                return Enumerable.Empty<IMigrationInfo>().ToArray();
            }

            var getAppliedMigrationsSql = @"SELECT index, description FROM migration_history";
            var appliedMigrations = await _connection.QueryAsync<MigrationInfo>(getAppliedMigrationsSql);

            return appliedMigrations.Cast<IMigrationInfo>().ToList();
        }

        private static void EnsureConsistent(
            ICollection<(IMigrationInfo Info, IMigration Migration)> possibleMigrations,
            ICollection<IMigrationInfo> appliedMigrations,
            short? migrationIndex)
        {
            if (possibleMigrations.Select(migration => migration.Info.Index).Distinct().Count() !=
                possibleMigrations.Count)
            {
                var message = "Some of the migrations registered in the application share the same index";
                throw new InconsistentMigrationsHistoryException(message);
            }
            
            if (migrationIndex.HasValue && possibleMigrations.All(migration => migration.Info.Index != migrationIndex))
            {
                var message = "An attempt was made to apply a non-existent (unregistered) migration";
                throw new InconsistentMigrationsHistoryException(message);
            }
            
            if (appliedMigrations.Count > possibleMigrations.Count)
            {
                var message = "Unknown (unregistered) migrations applied";
                throw new InconsistentMigrationsHistoryException(message);
            }
            
            foreach (var ((possibleMigration, _), appliedMigration) in possibleMigrations.Zip(appliedMigrations))
            {
                if (possibleMigration.Index != appliedMigration.Index ||
                    possibleMigration.Description != appliedMigration.Description)
                {
                    var message = "Applied migrations do not match the migrations specified in the application";
                    throw new InconsistentMigrationsHistoryException(message);
                }
            }
        }
        
        private async Task MigrateUpAsync(IEnumerable<(IMigrationInfo Info, IMigration Migration)> migrationsToApply)
        {
            await using var transaction = await _connection.BeginTransactionAsync();
            
            try
            {
                foreach (var (migrationInfo, migration) in migrationsToApply)
                {
                    Console.WriteLine($"Applying migration: #{migrationInfo.Index} \"{migrationInfo.Description}\".");

                    await _connection.ExecuteAsync(migration.Apply, null, transaction);

                    var addToHistorySql = @"INSERT INTO migration_history(index, description)
                                        VALUES(@Index, @Description)";
                    await _connection.ExecuteAsync(addToHistorySql, migrationInfo, transaction);

                    Console.WriteLine("Operation completed successfully.");
                    Console.WriteLine();
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                        
                Console.WriteLine("Operation failed.");
                Console.WriteLine();
                
                throw;
            }
        }

        private async Task MigrateDownAsync(IEnumerable<(IMigrationInfo Info, IMigration Migration)> migrationsToUndo)
        {
            await using var transaction = await _connection.BeginTransactionAsync();
            
            try
            {
                foreach (var (migrationInfo, migration) in migrationsToUndo)
                {
                    Console.WriteLine($"Undoing migration: #{migrationInfo.Index} \"{migrationInfo.Description}\".");

                    await _connection.ExecuteAsync(migration.Undo, null, transaction);

                    var deleteFomHistorySql = @"DELETE FROM migration_history WHERE index = @Index";
                    await _connection.ExecuteAsync(deleteFomHistorySql, migrationInfo, transaction);

                    Console.WriteLine("Operation completed successfully.");
                    Console.WriteLine();
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                        
                Console.WriteLine("Operation failed.");
                Console.WriteLine();
                
                throw;
            }
        }
    }
}
