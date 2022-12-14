using System.Collections.Generic;
using Fwks.Core.Domain;

namespace Fwks.Core.Abstractions.Mappers;

public interface INotificationContextCodeMapper
{
    int Get(IReadOnlyCollection<Notification> notifications);
}