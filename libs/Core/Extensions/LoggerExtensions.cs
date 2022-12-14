using System;
using Fwks.Core.Contexts;
using Microsoft.Extensions.Logging;

namespace Fwks.Core.Extensions;

public static class LoggerExtensions
{
    private const string UnexpectedError = "An unexpected error has ocurred. Check the messages and, if necessary, try again.";
    private const string CorrelationIdKey = "{CorrelationId}";
    private const string MessageKey = "{Message}";
    private const string DetailsKey = "{@Details}";

    public static void TraceCorrelatedUnexpectedError(this ILogger logger, Exception ex)
    {
        logger.TraceCorrelatedUnexpectedError(CorrelationContext.Id, ex);
    }

    public static void TraceCorrelatedUnexpectedError(this ILogger logger, string correlationId, Exception ex)
    {
        logger.LogError(ex, $"{CorrelationIdKey} {MessageKey}", correlationId, UnexpectedError);
    }

    public static void TraceCorrelatedInfo(this ILogger logger, string message, object details = default)
    {
        logger.TraceCorrelatedInfo(message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedInfo(this ILogger logger, string message, string correlationId, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Information, default, message, correlationId, details);
    }

    public static void TraceCorrelatedWarning(this ILogger logger, string message, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Warning, default, message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedWarning(this ILogger logger, string message, string correlationId, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Warning, default, message, correlationId, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, default, message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, string correlationId, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, default, message, correlationId, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, Exception ex, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, ex, message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, string correlationId, Exception ex, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, ex, message, correlationId, details);
    }

    public static void TraceUnexpectedError(this ILogger logger, Exception ex)
    {
        logger.LogError(ex, $"{MessageKey}", UnexpectedError);
    }

    public static void TraceInfo(this ILogger logger, string message, object details = default)
    {
        logger.Trace(LogLevel.Information, default, message, details);
    }

    public static void TraceWarning(this ILogger logger, string message, object details = default)
    {
        logger.Trace(LogLevel.Warning, default, message, details);
    }

    public static void TraceError(this ILogger logger, string message, object details = default)
    {
        logger.Trace(LogLevel.Error, default, message, details);
    }

    public static void TraceError(this ILogger logger, Exception ex, string message, object details = default)
    {
        logger.Trace(LogLevel.Error, ex, message, details);
    }

    public static void TraceCorrelated(this ILogger logger, LogLevel level, Exception ex, string message, string correlationId, object details = default)
    {
        logger.Trace(level, ex, message, details, correlationId);
    }

    public static void Trace(this ILogger logger, LogLevel level, Exception ex, string message, object details = default, string correlationId = default)
    {
        if (details == default)
        {
            if (correlationId == default)
                logger.Log(level, ex, MessageKey, message);
            else
                logger.Log(level, ex, $"{CorrelationIdKey} {MessageKey}", correlationId, message);
        }
        else
        {
            if (correlationId == default)
                logger.Log(level, ex, $"{MessageKey} {DetailsKey}", message, details);
            else
                logger.Log(level, ex, $"{CorrelationIdKey} {MessageKey} {DetailsKey}", correlationId, message, details);
        }
    }
}