using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Fwks.ExampleService.App.Api.Configuration;

internal static class SerilogConfiguration
{
    internal static void Initialize()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
            .AddEnvironmentVariables()
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}