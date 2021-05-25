namespace Catman.Blogger.Core.Persistence.UnitOfWork
{
    using System.Threading.Tasks;

    public interface IUnitOfWorkAsyncFactory
    {
        Task<IUnitOfWork> CreateAsync();
    }
}
