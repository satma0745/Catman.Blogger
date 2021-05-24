namespace Catman.Blogger.Persistence.Migrator.Exceptions
{
    using System;

    internal class ConnectionStringRequiredException : Exception
    {
        public override string Message =>
            "Connection string required (environment variable \"CATMAN_BLOGGER_CONNECTION_STRING\").";
    }
}
