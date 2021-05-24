namespace Catman.Blogger.Persistence.Migrator.Exceptions
{
    using System;

    internal class MigrationIndexMustBeNonNegativeException : Exception
    {
        public override string Message =>
            $"Migration index must be non-negative but got \"{_index}\".";

        private readonly short _index;

        public MigrationIndexMustBeNonNegativeException(short index)
        {
            _index = index;
        }
    }
}
