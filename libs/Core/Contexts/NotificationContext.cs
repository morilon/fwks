using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Abstractions.Mappers;
using Fwks.Core.Domain;

namespace Fwks.Core.Contexts;

public sealed class NotificationContext : INotificationContext
{
    private readonly INotificationContextCodeMapper _codeMapper;
    private readonly List<Notification> _notifications;

    public NotificationContext(
        INotificationContextCodeMapper codeMapper)
    {
        _codeMapper = codeMapper;
        _notifications = new();
    }

    public bool HasNotifications => _notifications.Any();
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public void Add(string message)
    {
        _notifications.Add(new() { Message = message });
    }

    public void Add(string code, string message)
    {
        _notifications.Add(new() { Code = code, Message = message });
    }

    public void Add(int code, string message)
    {
        Add(code.ToString(), message);
    }

    public void Add(IReadOnlyCollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void Add(params Notification[] notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void Add(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void Add<TNotification>(ValidationResult result) where TNotification : Notification, new()
    {
        var notifications = result.Errors.Select(x => new Notification { Message = x.ErrorMessage });

        _notifications.AddRange(notifications);
    }

    public int GetStatusCode()
    {
        return _codeMapper.Get(Notifications);
    }
}