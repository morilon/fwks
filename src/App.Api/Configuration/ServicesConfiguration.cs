using Fwks.ExampleService.Application;
using Microsoft.Extensions.DependencyInjection;
using Fwks.ExampleService.Infra.Mongo;
using Fwks.ExampleService.Core.Settings;
using Fwks.ExampleService.Infra.Postgres;

namespace Fwks.ExampleService.App.Api.Configuration;

internal static class DependenciesConfiguration
{
    internal static IServiceCollection AddDependencies(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddApplicationServices()
            .AddMongoDb(appSettings)
            .AddPostgres(appSettings);
    }
}