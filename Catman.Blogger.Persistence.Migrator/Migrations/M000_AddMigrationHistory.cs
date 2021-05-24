namespace Catman.Blogger.Persistence.Migrator.Migrations
{
    using Catman.Blogger.Persistence.Migrator.Migrations.Shared;

    [Migration(0, "Add migration history")]
    internal class M000_AddMigrationHistory : IMigration
    {
        public string Apply =>
            @"CREATE TABLE migration_history(
                  index smallint NOT NULL PRIMARY KEY,
                  description varchar(100),
                  applied_at timestamp with time zone NOT NULL DEFAULT current_timestamp,
                  CONSTRAINT index_non_negative CHECK (index >= 0)
              )";

        public string Undo =>
            @"DROP TABLE migration_history";
    }
}
