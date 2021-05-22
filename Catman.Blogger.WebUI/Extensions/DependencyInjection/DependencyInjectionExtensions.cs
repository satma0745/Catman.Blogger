namespace Catman.Blogger.WebUI.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class DependencyInjectionExtensions
    {
        public static void AddWebUI(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }
    }
}
