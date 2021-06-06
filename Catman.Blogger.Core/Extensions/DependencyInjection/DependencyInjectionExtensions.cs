namespace Catman.Blogger.Core.Extensions.DependencyInjection
{
    using Catman.Blogger.Core.Services.Blog;
    using Catman.Blogger.Core.Services.User;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services) =>
            services
                .AddScoped<IBlogService, BlogService>()
                .AddScoped<IUserService, UserService>();
    }
}
