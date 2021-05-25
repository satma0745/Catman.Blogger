namespace Catman.Blogger.WebUI
{
    using Catman.Blogger.Core.Extensions.DependencyInjection;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Core.Services.Shared.Responses;
    using Catman.Blogger.Persistence.Extensions.DependencyInjection;
    using Catman.Blogger.WebUI.Extensions.ApplicationConfiguration;
    using Catman.Blogger.WebUI.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddAutoMapper(typeof(IUnitOfWork), typeof(IResponse))
                .AddPersistence(_configuration)
                .AddCore()
                .AddWebUI();

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application
                .UseStaticFiles()
                .UseRouting()
                .UseBlazorEndpoints();
        }
    }
}
