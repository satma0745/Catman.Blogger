namespace Catman.Blogger.Core.Persistence.UnitOfWork
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.Blog;

    public interface IUnitOfWork : IAsyncDisposable
    {
        IBlogRepository Blogs { get; }

        Task CommitAsync();

        Task RollbackAsync();
    }
}
