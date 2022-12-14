using System;
using Fwks.Core.Extensions;

namespace Fwks.ExampleService.Core.Settings;

public sealed class AppSettings
{
    private string _environment;

    public AuthServerSettings AuthServer { get; set; }
    public CorsSettings Cors { get; set; }
    public StorageSettings Storage { get; set; }
    public SecuritySettings Security { get; set; }

    public bool IsProduction()
    {
        if (_environment.IsEmpty())
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return _environment == "Production";
    }
}