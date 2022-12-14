using Fwks.ExampleService.Core.Abstractions.Repositories;
using Fwks.ExampleService.Core.Domain;
using Fwks.ExampleService.Core.Settings;
using Fwks.ExampleService.Infra.Postgres.Contexts;
using Fwks.ExampleService.Infra.Postgres.Repositories;
using Fwks.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.ExampleService.Infra.Postgres;

public static class ConfigureServices
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddPostgres<AppServiceContext>(appSettings.Storage.Postgres)
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerRepository<Customer, int>, CustomerRepository>();
    }
}