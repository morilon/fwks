using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fwks.Core.Abstractions.Mappers;
using Fwks.Core.Domain;

namespace Fwks.Core.Mappers;

public sealed class DefaultNotificationContextCodeMapper : INotificationContextCodeMapper
{
    public int Get(IReadOnlyCollection<Notification> notifications)
    {
        var internalServerError = (int)HttpStatusCode.InternalServerError;

        if (notifications.Count > 1)
            return internalServerError;

        var code = notifications.First().Code;

        if (!int.TryParse(code, out int statusCode))
            return internalServerError;

        return Enum.GetValues<HttpStatusCode>().Cast<int>().Any(x => x == statusCode)
            ? statusCode
            : internalServerError;
    }
}