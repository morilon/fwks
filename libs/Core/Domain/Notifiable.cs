using System.Collections.Generic;
using System.Linq;

namespace Fwks.Core.Domain;

public abstract class Notifiable
{
    private readonly List<Notification> _notifications;

    public Notifiable()
    {
        _notifications = new List<Notification>();
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public void AddNotification(string message)
    {
        _notifications.Add(new() { Message = message });
    }

    public void AddNotification(string code, string message)
    {
        _notifications.Add(new() { Code = code, Message = message });
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(params string[] messages)
    {
        messages.ToList().ForEach(AddNotification);
    }

    public void AddNotifications(params Notification[] notifications)
    {
        notifications.ToList().ForEach(AddNotification);
    }
}