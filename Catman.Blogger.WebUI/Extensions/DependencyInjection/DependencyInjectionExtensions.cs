namespace Catman.Blogger.WebUI.Extensions.DependencyInjection
{
    using Catman.Blogger.WebUI.Data.Blog;
    using Catman.Blogger.WebUI.Data.User;
    using Microsoft.Extensions.DependencyInjection;

    internal static class DependencyInjectionExtensions
    {
        public static void AddWebUI(this IServiceCollection services)
        {
            services.AddStorage();
            
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        private static IServiceCollection AddStorage(this IServiceCollection services) =>
            services
                .AddScoped<IBlogStorage, BlogStorage>()
                .AddScoped<IUserStorage, UserStorage>();
    }
}
