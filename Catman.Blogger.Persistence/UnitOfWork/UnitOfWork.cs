namespace Catman.Blogger.Persistence.UnitOfWork
{
    using System;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Catman.Blogger.Persistence.Repositories.Blog;

    public class UnitOfWork : IDisposable
    {
        public BlogRepository Blogs { get; }
        
        private readonly DbTransaction _transaction;
        
        public UnitOfWork(DbConnection connection)
        {
            _transaction = connection.BeginTransaction();

            Blogs = new BlogRepository(connection, _transaction);
        }

        public Task CommitAsync() =>
            _transaction.CommitAsync();

        public Task RollbackAsync() =>
            _transaction.RollbackAsync();

        public void Dispose() =>
            _transaction.Dispose();
    }
}
