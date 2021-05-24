namespace Catman.Blogger.Persistence.Migrator.Exceptions
{
    using System;

    internal class InconsistentMigrationsHistoryException : Exception
    {
        public override string Message =>
            $"Migrations inconsistency: \"{_cause}\".";

        private readonly string _cause;

        public InconsistentMigrationsHistoryException(string cause)
        {
            _cause = cause;
        }
    }
}
