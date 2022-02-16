using MicroAutomation.Log.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MicroAutomation.Log.Extensions;

public static class LoggerConfigurationExtensions
{
    /// <summary>
    /// Add serilog with custom implementation.
    /// </summary>
    /// <param name="hostBuilder"></param>
    public static void UseCustomSerilog(this HostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((hostContext, services, loggerConfig) =>
        {
            loggerConfig.ConfigureSerilog(hostContext);
        });
    }

    /// <summary>
    /// Add serilog with custom implementation.
    /// </summary>
    /// <param name="hostBuilder"></param>
    public static void UseCustomSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((hostContext, services, loggerConfig) =>
        {
            loggerConfig.ConfigureSerilog(hostContext);
        });
    }

    /// <summary>
    /// Add serilog with custom implementation.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="hostContext"></param>
    public static void ConfigureSerilog(
        this LoggerConfiguration logger,
        HostBuilderContext hostContext)
    {
        // Add loki configuration.
        var logConfiguration = new LogOption();
        hostContext.Configuration.GetSection(nameof(LogOption)).Bind(logConfiguration);

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