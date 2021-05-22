namespace Catman.Blogger.WebUI.Extensions.ApplicationConfiguration
{
    using Microsoft.AspNetCore.Builder;

    internal static class EndpointsConfigurationExtensions
    {
        public static void UseBlazorEndpoints(this IApplicationBuilder application) =>
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
    }
}
