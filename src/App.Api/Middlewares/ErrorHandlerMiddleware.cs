using System;
using System.Threading.Tasks;
using Fwks.Core.Constants;
using Fwks.Core.Contexts;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Fwks.ExampleService.App.Api.Middlewares;

internal sealed class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(
        ILogger<ErrorHandlerMiddleware> logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleUnexpectedExceptionAsync(context, ex);
        }
    }

    private Task HandleUnexpectedExceptionAsync(HttpContext context, Exception ex)
    {
        CorrelationContext.SetFromHeaders(context.Request.Headers);

        CorrelationContext.AddToHeaders(context.Response.Headers);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        _logger.TraceCorrelatedUnexpectedError(ex);

        return context.Response.WriteAsJsonAsync(ApplicationNotification.Create(ApplicationErrorMessages.SomethingWentWrong));
    }
}