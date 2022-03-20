# Netboot - Logging

Micro library for my automation utilities.

## Project Status

[![Tests](https://github.com/NetbootCompany/Netboot-Logging/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/NetbootCompany/Netboot-Logging/actions/workflows/build-and-test.yml)
[![Publish Nuget package](https://github.com/NetbootCompany/Netboot-Logging/actions/workflows/build-and-publish-nuget.yml/badge.svg)](https://github.com/NetbootCompany/Netboot-Logging/actions/workflows/build-and-publish-nuget.yml)

[![Reliability Rating](https://sonarqube.netboot.fr/api/project_badges/measure?project=netboot_logging&metric=reliability_rating)](https://sonarqube.netboot.fr/dashboard?id=netboot_logging)
[![Security Rating](https://sonarqube.netboot.fr/api/project_badges/measure?project=netboot_logging&metric=security_rating)](https://sonarqube.netboot.fr/dashboard?id=netboot_logging)
[![Code Smells](https://sonarqube.netboot.fr/api/project_badges/measure?project=netboot_logging&metric=code_smells)](https://sonarqube.netboot.fr/dashboard?id=netboot_logging)

## Configuration

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netboot.Logging.Extensions;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Set current directory.
// In order to create a log folder in the correct directory.
var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
var pathContextRoot = new FileInfo(location.AbsolutePath).Directory.FullName;
builder.Host.UseContentRoot(pathContextRoot);
Directory.SetCurrentDirectory(pathContextRoot);

// Add serilog implementation.
builder.Host.UseCustomSerilog();

...
```

## JSON (Microsoft.Extensions.Configuration)

Keys and values are not case-sensitive. This is an example of configuring the utility arguments from `Appsettings.json`:

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

For bugs, questions and discussions please use the [GitHub Issues](https://github.com/NetbootCompany/Netboot-Logging/issues).

## License

This project is licensed under [MIT License](https://github.com/NetbootCompany/Netboot-Logging/blob/main/LICENSE).
