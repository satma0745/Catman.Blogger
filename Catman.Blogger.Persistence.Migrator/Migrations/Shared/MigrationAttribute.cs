namespace Catman.Blogger.Persistence.Migrator.Migrations.Shared
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    internal class MigrationAttribute : Attribute, IMigrationInfo
    {
        public short Index { get; }
        
        public string Description { get; }

        public MigrationAttribute(short index, string description = null)
        {
            Index = index;
            Description = description;
        }
    }
}
