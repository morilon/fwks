using System;
using System.Threading.Tasks;
using Fwks.Core.Contexts;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Fwks.AspNetCore.Extensions;

public static class JwtBearerEventsExtensions
{
    public static Func<AuthenticationFailedContext, Task> OnAuthenticationFailed(ILogger logger, string message = default)
    {
        return (context) =>
        {
            context.Response.OnStarting(() =>
            {
                message ??= "Failed to validate token.";

                CorrelationContext.SetFromHeaders(context.HttpContext.Request.Headers);

                CorrelationContext.AddToHeaders(context.HttpContext.Response.Headers);

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                logger.TraceCorrelatedError(message, context.Exception);

                return context.Response.WriteAsJsonAsync(ApplicationNotification.Create(message));
            });

            return Task.CompletedTask;
        };
    }

    public static Func<ForbiddenContext, Task> OnForbidden(ILogger logger, string message = default)
    {
        return (context) =>
        {
            context.Response.OnStarting(() =>
            {
                message ??= "Failed to authorize the request. Forbidden.";

                CorrelationContext.SetFromHeaders(context.HttpContext.Request.Headers);

                CorrelationContext.AddToHeaders(context.HttpContext.Response.Headers);

                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                logger.TraceCorrelatedError(message, context.Response);

                return context.Response.WriteAsJsonAsync(ApplicationNotification.Create(message));
            });

            return Task.CompletedTask;
        };
    }
}