namespace Catman.Blogger.Persistence.Migrator
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Persistence.Migrator.Exceptions;
    using Catman.Blogger.Persistence.Migrator.Runner;
    using CommandLine;
    using Npgsql;

    internal class Program
    {
        private class CliOptions
        {
            [Option('m', "migration", Required = false, HelpText = "Specifies the id of the migration to migrate to.")]
            public short? MigrationId { get; set; }
        }
        
        private static Task Main(string[] arguments) =>
            Parser.Default
                .ParseArguments<CliOptions>(arguments)
                .WithParsedAsync(options => Migrate(options.MigrationId));

        private static async Task Migrate(short? migrationId)
        {
            var connectionString = Environment.GetEnvironmentVariable("CATMAN_BLOGGER_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ConnectionStringRequiredException();
            }

            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var migrationRunner = new MigrationRunner(connection);
            await migrationRunner.MigrateAsync(migrationId);
        }
    }
}
