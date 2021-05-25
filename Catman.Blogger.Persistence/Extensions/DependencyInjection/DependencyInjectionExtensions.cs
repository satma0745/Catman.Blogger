namespace Catman.Blogger.Persistence.Extensions.DependencyInjection
{
    using System.Data.Common;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Persistence.UnitOfWork;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Npgsql;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            
            var connectionString = configuration["CATMAN_BLOGGER_CONNECTION_STRING"];
            
            return services
                .AddScoped<DbConnection>(_ => new NpgsqlConnection(connectionString))
                .AddScoped<IUnitOfWorkAsyncFactory, UnitOfWorkAsyncFactory>();
        }
    }
}
