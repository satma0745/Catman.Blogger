namespace Catman.Blogger.Core.Persistence.UnitOfWork
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.Blog;
    using Catman.Blogger.Core.Persistence.Repositories.User;

    public interface IUnitOfWork : IAsyncDisposable
    {
        IBlogRepository Blogs { get; }
        
        IUserRepository Users { get; }

        Task CommitAsync();

        Task RollbackAsync();
    }
}
