namespace Fwks.AspNetCore.Middlewares.BuildInfo.Models;

public class BuildInfoEndpointOptions
{
    public string Route { get; set; } = "/version";
    public bool RequireAuthorization { get; set; } = false;
}