using System.Threading.Tasks;
using Fwks.AspNetCore.Middlewares.BuildInfo.Models;
using Microsoft.AspNetCore.Http;

namespace Fwks.AspNetCore.Middlewares.BuildInfo;

public class BuildInfoEndpointMiddleware
{
    private readonly RequestDelegate _next;
    private readonly BuildInfoEndpointOptions _settings;

    public BuildInfoEndpointMiddleware(
        RequestDelegate next,
        BuildInfoEndpointOptions settings)
    {
        _next = next;
        _settings = settings;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (!await TryGetVersion(context))
            {
                await _next(context);
            }
        }
        catch
        {
            throw;
        }
    }

    private async Task<bool> TryGetVersion(HttpContext context)
    {
        if (_settings.RequireAuthorization && !context.User.Identity.IsAuthenticated)
        {
            return false;
        }

        context.Response.StatusCode = StatusCodes.Status200OK;

        await context.Response.WriteAsJsonAsync(BuildInfoModel.Create());

        return true;
    }
}