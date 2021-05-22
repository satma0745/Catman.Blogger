namespace Catman.Blogger.WebUI
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] arguments) =>
            CreateHostBuilder(arguments).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] arguments) =>
            Host.CreateDefaultBuilder(arguments)
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}
