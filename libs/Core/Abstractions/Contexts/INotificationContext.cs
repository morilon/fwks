using System.Collections.Generic;
using FluentValidation.Results;
using Fwks.Core.Domain;

namespace Fwks.Core.Abstractions.Contexts;

public interface INotificationContext
{
    bool HasNotifications { get; }
    IReadOnlyCollection<Notification> Notifications { get; }

    void Add(string message);
    void Add(string code, string message);
    void Add(int code, string message);
    void Add(IReadOnlyCollection<Notification> notifications);
    void Add(params Notification[] notifications);
    void Add(Notification notification);
    void Add<TNotification>(ValidationResult result) where TNotification : Notification, new();
    int GetStatusCode();
}