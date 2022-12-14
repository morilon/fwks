using System;
using Fwks.ExampleService.Core.Abstractions.Repositories;
using Fwks.ExampleService.Core.Domain;
using Fwks.ExampleService.Core.Settings;
using Fwks.ExampleService.Infra.Mongo.Abstractions;
using Fwks.ExampleService.Infra.Mongo.Repositories;
using Fwks.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.ExampleService.Infra.Mongo;

public static class ConfigureServices
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, AppSettings appSettings)
    {
        return services
             .AddMongoDb<IEntityMap>(appSettings.Storage.MongoDb, appSettings.Storage.MongoDb.Database)
             .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerRepository<CustomerDocument, Guid>, CustomerRepository>();
    }
}