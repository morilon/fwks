using Fwks.Core.Extensions;
using Microsoft.AspNetCore.Routing;

namespace Fwks.AspNetCore.Transformers;

public sealed class SlugCaseParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        return value.ToString().ToSlugCase();
    }
}