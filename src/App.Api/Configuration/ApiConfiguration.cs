using System.IO.Compression;
using System.Net.Mime;
using FluentValidation;
using FluentValidation.AspNetCore;
using Fwks.AspNetCore.Attributes;
using Fwks.AspNetCore.Filters.ResultFilters;
using Fwks.AspNetCore.Transformers;
using Fwks.ExampleService.App.Api.Configuration;
using Fwks.ExampleService.Core.Configuration;
using Fwks.ExampleService.Core.Domain.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.ExampleService.App.Api.Configuration;

internal static class MediatorConfiguration
{
    internal static IServiceCollection AddMediator(this IServiceCollection services)
    {
        return services;
    }
}

internal static class ApiConfiguration
{
    internal static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        return services
            .AddHttpClient()
            .AddResponseCompression()
            .AddVersioning()
            .AddFluentValidation()
            .AddControllersConfiguration();
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        return services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(CustomerResponse).Assembly)
            .Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);
    }

    private static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        return services
            .AddApiVersioning(x =>
            {
                x.ReportApiVersions = true;
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddVersionedApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'V";
                x.SubstituteApiVersionInUrl = true;
            });
    }

    private static IServiceCollection AddResponseCompression(this IServiceCollection services)
    {
        return services
            .Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal)
            .AddResponseCompression(x =>
            {
                x.EnableForHttps = true;
                x.Providers.Add<GzipCompressionProvider>();
            });
    }

    private static IServiceCollection AddControllersConfiguration(this IServiceCollection services)
    {
        return services
            .AddControllers(x =>
            {
                x.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                x.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
                x.Filters.Add(new ProducesApplicationNotificationResponseAttribute(StatusCodes.Status500InternalServerError));
                x.Filters.Add(new ProducesApplicationNotificationResponseAttribute(StatusCodes.Status401Unauthorized));
                x.Filters.Add<NotificationResultFilter>();

                x.Conventions.Add(new RouteTokenTransformerConvention(new SlugCaseParameterTransformer()));
            })
            .AddJsonOptions(x => JsonSerializerConfiguration.Configure(x.JsonSerializerOptions))
            .Services;
    }
}