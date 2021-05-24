namespace Catman.Blogger.Persistence.Migrator.Exceptions
{
    using System;

    internal class MigrationAttributeRequiredException : Exception
    {
        public override string Message => 
            $"\"{_migrationClassName}\" must have MigrationAttribute.";
        
        private readonly string _migrationClassName;
        
        public MigrationAttributeRequiredException(string migrationClassName)
        {
            _migrationClassName = migrationClassName;
        }
    }
}
