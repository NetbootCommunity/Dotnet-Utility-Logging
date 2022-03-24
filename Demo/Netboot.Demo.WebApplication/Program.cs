using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Netboot.Logging.Extensions;

namespace Netboot.Demo.WebApplication
{
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // Initializes a new instance with pre-configured defaults.
            var builder = Host.CreateDefaultBuilder(args);

            // Add serilog implementation.
            builder.UseCustomSerilog();

            // Configures a IHostBuilder with defaults for hosting a web app.
            builder.ConfigureWebHostDefaults(webBuilder
                => webBuilder.UseStartup<Startup>().UseKestrel());

            return builder;
        }
    }
}