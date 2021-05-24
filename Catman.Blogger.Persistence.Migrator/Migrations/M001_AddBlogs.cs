namespace Catman.Blogger.Persistence.Migrator.Migrations
{
    using Catman.Blogger.Persistence.Migrator.Migrations.Shared;

    [Migration(1, "Add blogs")]
    internal class M001_AddBlogs : IMigration
    {
        public string Apply =>
            @"CREATE TABLE blogs(
                  id uuid NOT NULL DEFAULT gen_random_uuid() PRIMARY KEY,
                  title varchar(100) NOT NULL UNIQUE,
                  description varchar(250) NOT NULL,
                  creation_date timestamp with time zone NOT NULL DEFAULT current_timestamp
              )";

        public string Undo =>
            @"DROP TABLE blogs";
    }
}
