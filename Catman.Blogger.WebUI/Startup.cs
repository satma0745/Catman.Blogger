namespace Catman.Blogger.WebUI
{
    using Catman.Blogger.WebUI.Extensions.ApplicationConfiguration;
    using Catman.Blogger.WebUI.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) =>
            services.AddWebUI();

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application
                .UseStaticFiles()
                .UseRouting()
                .UseBlazorEndpoints();
        }
    }
}
