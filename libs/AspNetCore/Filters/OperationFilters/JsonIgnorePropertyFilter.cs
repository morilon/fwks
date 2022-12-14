using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fwks.AspNetCore.Filters.OperationFilters;

public sealed class JsonIgnorePropertyFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parametersToIgnore = context.MethodInfo.GetParameters()
            .SelectMany(x => x.ParameterType.GetProperties().Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() != null))
            .Select(x => x.Name);

        if (!parametersToIgnore.Any())
            return;

        operation.Parameters = operation.Parameters.Where(x =>
            !parametersToIgnore.Any(p =>
                p.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))).ToList();
    }
}