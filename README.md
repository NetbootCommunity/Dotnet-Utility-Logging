# Netboot - Logging [![Build](https://github.com/NetbootCommunity/Netboot-Logging/actions/workflows/build.yml/badge.svg)](https://github.com/NetbootCommunity/Netboot-Logging/actions/workflows/build.yml) [![NuGet Version](http://img.shields.io/nuget/v/Netboot.Logging.svg?style=flat)](https://www.nuget.org/packages/Netboot.Logging/)  [![Reliability Rating](https://sonarqube.netboot.fr/api/project_badges/measure?project=netboot_logging&metric=reliability_rating)](https://sonarqube.netboot.fr/dashboard?id=netboot_logging) [![Security Rating](https://sonarqube.netboot.fr/api/project_badges/measure?project=netboot_logging&metric=security_rating)](https://sonarqube.netboot.fr/dashboard?id=netboot_logging) [![Code Smells](https://sonarqube.netboot.fr/api/project_badges/measure?project=netboot_logging&metric=code_smells)](https://sonarqube.netboot.fr/dashboard?id=netboot_logging)

This project is an add-on to [Serilog](https://serilog.net) to easily configure the logging.

> This product is part of netboot utilities for its own solution, you can freely use this project at your own risk.

## Please show the value

Choosing a project dependency could be difficult. We need to ensure stability and maintainability of our projects.
Surveys show that GitHub stars count play an important factor when assessing library quality.

⭐ Please give this repository a star. It takes seconds and help thousands of developers! ⭐

## Support development

It doesn't matter if you are a professional developer, creating a startup or work for an established company.
All of us care about our tools and dependencies, about stability and security, about time and money we can safe, about quality we can offer.
Please consider sponsoring to give me an extra motivational push to develop the next great feature.

> If you represent a company, want to help the entire community and show that you care, please consider sponsoring using one of the higher tiers.
Your company logo will be shown here for all developers, building a strong positive relation.

## Installation

The library is available as a nuget package. You can install it as any other nuget package from your IDE, try to search by Netboot.Logging.
You can find package details [on this webpage](https://www.nuget.org/packages/Netboot.Logging).

```xml
// Package Manager
Install-Package Netboot.Logging

// .NET CLI
dotnet add package Netboot.Logging

// Package reference in .csproj file
<PackageReference Include="Netboot.Logging" Version="6.2.0" />
```

## Program Configuration

While a .NET 6 console app template generates the new style of top-level statements programs, using .NET 5 doesn't.
By creating a .NET 5 project, you'll receive the old program style.
Then, you can edit the project file to target .NET 6 but retain the old program style for the Program.cs file.

### New program style

```csharp
// Create web application builder.
var builder = WebApplication.CreateBuilder(args);

// Add serilog implementation.
builder.Host.UseCustomSerilog();

...
```

### Old program style

```csharp
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
```

## JSON Configuration

You can override the default configuration for this product by adding settings in your `Appsettings.json`.
The example below therefore allows you to override the configuration.

```json
{
  "LogConfiguration": {
    "File": {
      "Status": true,
      "Path": "Logs\\log-.txt",
      "FileSizeLimitBytes": 5242880,
      "RollingInterval": "Day",
      "RollOnFileSizeLimit": true,
      "RetainedFileCountLimit": 30
    },
    "Console": {
      "Status": true,
      "OutputTemplate": "[{Timestamp:HH:mm:ss.fff}][{Level:u4}][{ThreadId}][{SourceContext}] {Message}{NewLine}{Exception}",
      "BufferSize": 10000,
      "BlockWhenFull": true
    }
  }
}
```

## How to Contribute

Everyone is welcome to contribute to this project! Feel free to contribute with pull requests, bug reports or enhancement suggestions.

## Bugs and Feedback

For bugs, questions and discussions please use the [GitHub Issues](https://github.com/NetbootCommunity/Netboot-Logging/issues).

## License

This project is licensed under [MIT License](https://github.com/NetbootCommunity/Netboot-Logging/blob/main/LICENSE).
