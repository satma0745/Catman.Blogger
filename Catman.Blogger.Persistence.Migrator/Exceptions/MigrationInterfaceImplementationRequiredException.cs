namespace Catman.Blogger.Persistence.Migrator.Exceptions
{
    using System;

    internal class MigrationInterfaceImplementationRequiredException : Exception
    {
        public override string Message =>
            $"\"{_migrationClassName}\" must implement IMigration interface.";
        
        private readonly string _migrationClassName;

        public MigrationInterfaceImplementationRequiredException(string migrationClassName)
        {
            _migrationClassName = migrationClassName;
        }
    }
}
