namespace Catman.Blogger.Persistence.UnitOfWork
{
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public class UnitOfWorkFactory
    {
        private readonly DbConnection _connection;

        public UnitOfWorkFactory(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<UnitOfWork> CreateAsync()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }

            return new UnitOfWork(_connection);
        }
    }
}
