using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fwks.ExampleService.Core.Configuration;

public static class JsonSerializerConfiguration
{
    private static JsonSerializerOptions _default;

    public static JsonSerializerOptions Default
    {
        get
        {
            if (_default != default)
                return _default;

            _default = new JsonSerializerOptions();

            Configure(_default);

            return _default;
        }
    }

    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.Converters.Add(new JsonStringEnumConverter());
    }
}