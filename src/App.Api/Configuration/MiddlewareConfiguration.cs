using Fwks.AspNetCore.Middlewares.BuildInfo.Extensions;
using Fwks.ExampleService.App.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Fwks.ExampleService.App.Api.Configuration;

internal static class MiddlewareConfiguration
{
    internal static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        return app
            .UseBuildInfoEndpoint()
            .UseMiddleware<CorrelationIdMiddleware>()
            .UseMiddleware<ErrorHandlerMiddleware>();
    }
}