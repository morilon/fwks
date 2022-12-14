using Fwks.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Fwks.AspNetCore.Attributes;

public sealed class ProducesApplicationNotificationResponseAttribute : ProducesResponseTypeAttribute
{
    public ProducesApplicationNotificationResponseAttribute(int statusCode)
        : base(typeof(ApplicationNotification), statusCode)
    {
    }
}