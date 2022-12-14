using System.Collections.Generic;

namespace Fwks.AspNetCore.Options;

public sealed class OAuth2SecurityOptions
{
    public string Authority { get; set; } = default!;
    public IDictionary<string, string> Scopes { get; set; } = default!;
    public bool AddClientCredentials { get; set; }
    public string Name { get; set; } = "OAuth2";
    public string Description { get; set; } = "OAuth2 Authentication";
}