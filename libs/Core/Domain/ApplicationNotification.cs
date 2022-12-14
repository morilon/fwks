using System.Collections.Generic;
using System.Linq;
using Fwks.Core.Constants;

namespace Fwks.Core.Domain;

public sealed class ApplicationNotification
{
    public required string Message { get; set; }
    public required List<string> Notifications { get; set; } = new();

    public static ApplicationNotification Create(string message, params string[] notifications)
    {
        return new()
        {
            Message = message,
            Notifications = notifications.ToList()
        };
    }

    public static ApplicationNotification Create(string message, IReadOnlyCollection<Notification> notifications)
    {
        return Create(message, notifications.Select(x => x.Message).ToArray());
    }

    public static ApplicationNotification Create(IReadOnlyCollection<Notification> notifications)
    {
        return Create(ApplicationErrorMessages.CheckNotifications, notifications.ToList());
    }

    public static ApplicationNotification InternalError()
    {
        return Create(ApplicationErrorMessages.SomethingWentWrong, "An unexpected error has occurred, check the logs for more information.");
    }
}