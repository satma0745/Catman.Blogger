namespace Catman.Blogger.Persistence.Migrator.Migrations
{
    using Catman.Blogger.Persistence.Migrator.Migrations.Shared;
    
    [Migration(2, "Add users")]
    internal class M002_AddUsers : IMigration
    {
        public string Apply =>
            @"CREATE TABLE users(
                  id uuid NOT NULL DEFAULT gen_random_uuid() PRIMARY KEY,
                  username varchar(20) NOT NULL UNIQUE,
                  password varchar(20) NOT NULL,
                  display_name varchar(30) NOT NULL
              )";

        public string Undo =>
            @"DROP TABLE users";
    }
}
