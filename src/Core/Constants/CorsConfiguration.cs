namespace Fwks.ExampleService.Core.Constants;

public static class CorsConfiguration
{
    public static string PolicyName => "DefaultCorsPolicy";
    public static string[] AllowedMethods => new[] { "GET", "POST", "PUT", "PATCH", "DELETE" };
}