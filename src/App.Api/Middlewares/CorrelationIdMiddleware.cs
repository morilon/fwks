using System.Threading.Tasks;
using Fwks.Core.Contexts;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Fwks.ExampleService.App.Api.Middlewares;

internal sealed class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CorrelationContext.SetFromHeaders(context.Request.Headers);

        using (LogContext.PushProperty(CorrelationContext.PropertyName, CorrelationContext.Id))
            await _next.Invoke(context);
    }
}