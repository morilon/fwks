using System;
using Fwks.AspNetCore.Middlewares.BuildInfo.Models;
using Microsoft.AspNetCore.Builder;

namespace Fwks.AspNetCore.Middlewares.BuildInfo.Extensions;

public static class BuildVersionExtensions
{
    public static IApplicationBuilder UseBuildInfoEndpoint(this IApplicationBuilder appBuilder, Action<BuildInfoEndpointOptions> optionsAction = null)
    {
        var options = new BuildInfoEndpointOptions();

        optionsAction?.Invoke(options);

        return appBuilder
            .MapWhen(
                context => context.Request.Path.Equals(options.Route, StringComparison.InvariantCultureIgnoreCase),
                app => app.UseMiddleware<BuildInfoEndpointMiddleware>(options));
    }
}