namespace Catman.Blogger.Persistence.UnitOfWork
{
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.Blog;
    using Catman.Blogger.Core.Persistence.Repositories.User;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Persistence.Repositories;

    internal class UnitOfWork : IUnitOfWork
    {
        public IBlogRepository Blogs { get; }
        
        public IUserRepository Users { get; }
        
        private readonly DbTransaction _transaction;
        
        public UnitOfWork(IDbConnection connection, DbTransaction transaction)
        {
            _transaction = transaction;

            Blogs = new BlogRepository(connection, _transaction);
            Users = new UserRepository(connection, _transaction);
        }

        public Task CommitAsync() =>
            _transaction.CommitAsync();

        public Task RollbackAsync() =>
            _transaction.RollbackAsync();

        public ValueTask DisposeAsync() =>
            _transaction.DisposeAsync();
    }
}
