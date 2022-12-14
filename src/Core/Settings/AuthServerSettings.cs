using System.Collections.Generic;

namespace Fwks.ExampleService.Core.Settings;

public sealed class AuthServerSettings
{
    public string Audience { get; set; }
    public string Authority { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public IDictionary<string, string> Scopes { get; set; }
}