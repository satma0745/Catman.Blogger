namespace Catman.Blogger.Persistence.UnitOfWork
{
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.UnitOfWork;

    internal class UnitOfWorkAsyncFactory : IUnitOfWorkAsyncFactory
    {
        private readonly DbConnection _connection;

        public UnitOfWorkAsyncFactory(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IUnitOfWork> CreateAsync()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }

            var transaction = await _connection.BeginTransactionAsync();
            return new UnitOfWork(_connection, transaction);
        }
    }
}
