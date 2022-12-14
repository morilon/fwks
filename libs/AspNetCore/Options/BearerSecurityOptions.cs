namespace Fwks.AspNetCore.Options;

public sealed class BearerSecurityOptions
{
    public string Name { get; set; } = "Bearer";
    public string Description { get; set; } = "Bearer Token Authorization";
}