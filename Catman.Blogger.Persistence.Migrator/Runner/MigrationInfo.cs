namespace Catman.Blogger.Persistence.Migrator.Runner
{
    using Catman.Blogger.Persistence.Migrator.Migrations.Shared;

    internal class MigrationInfo : IMigrationInfo
    {
        public short Index { get; set; }
        
        public string Description { get; set; }
    }
}
