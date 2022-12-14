using Fwks.Core;
using Fwks.ExampleService.Application.Services;
using Fwks.ExampleService.Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.ExampleService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddNotificationContext()
            .AddScoped<ICustomerService, CustomerService>();
    }
}