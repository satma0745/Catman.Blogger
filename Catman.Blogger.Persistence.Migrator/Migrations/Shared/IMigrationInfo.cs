namespace Catman.Blogger.Persistence.Migrator.Migrations.Shared
{
    internal interface IMigrationInfo
    {
        short Index { get; }
        
        string Description { get; }
    }
}
