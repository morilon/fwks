using Fwks.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Fwks.AspNetCore.Attributes;

public sealed class ProducesPagedResponseAttribute<T> : ProducesResponseTypeAttribute where T : class
{
    public ProducesPagedResponseAttribute(int statusCode)
        : base(typeof(Page<T>), statusCode)
    {
    }
}