namespace Catman.Blogger.WebUI
{
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
                .AddPersistence(_configuration)
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
