using Netboot.Logging.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Netboot.Logging.Extensions
{
    public static class LoggerConfigurationExtensions
    {
        /// <summary>
        /// Add serilog with custom implementation.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns></returns>
        public static HostBuilder UseCustomSerilog(this HostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostContext, services, loggerConfig) =>
            {
                loggerConfig.ConfigureSerilog(hostContext);
            });

            return hostBuilder;
        }

        /// <summary>
        /// Add serilog with custom implementation.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns></returns>
        public static IHostBuilder UseCustomSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostContext, services, loggerConfig) =>
            {
                loggerConfig.ConfigureSerilog(hostContext);
            });

            return hostBuilder;
        }

        /// <summary>
        ///  Add serilog with custom implementation.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="hostContext">The host context.</param>
        public static void ConfigureSerilog(this LoggerConfiguration logger, HostBuilderContext hostContext)
        {
            // Add loki configuration.
            var logConfiguration = new LogConfiguration();
            hostContext.Configuration.GetSection(nameof(LogConfiguration)).Bind(logConfiguration);

            // Add user configuration.
            var config = logger.ReadFrom.Configuration(hostContext.Configuration);

            // Add console configuration.
            if (logConfiguration.Console.Status)
            {
                config.WriteTo.Async(opt => opt.Console(
                outputTemplate: logConfiguration.Console.OutputTemplate),
                bufferSize: logConfiguration.Console.BufferSize,
                blockWhenFull: logConfiguration.Console.BlockWhenFull);
            }

            // Add file configuration.
            if (logConfiguration.File.Status)
            {
                config.WriteTo.File(
                path: logConfiguration.File.Path,
                fileSizeLimitBytes: logConfiguration.File.FileSizeLimitBytes,
                rollingInterval: logConfiguration.File.RollingInterval,
                rollOnFileSizeLimit: logConfiguration.File.RollOnFileSizeLimit,
                retainedFileCountLimit: logConfiguration.File.RetainedFileCountLimit);
            }

            // Add default enrichments.
            config.Enrich.WithDefault();
            config.Enrich.WithCustomImplementation();
        }
    }
}