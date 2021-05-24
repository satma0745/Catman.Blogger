namespace Catman.Blogger.Persistence.Migrator.Migrations.Shared
{
    internal interface IMigration
    {
        string Apply { get; }
        
        string Undo { get; }
    }
}
