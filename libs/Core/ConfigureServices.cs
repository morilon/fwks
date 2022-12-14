using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Abstractions.Mappers;
using Fwks.Core.Contexts;
using Fwks.Core.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddNotificationContext(this IServiceCollection services)
    {
        return services
            .AddSingleton<INotificationContextCodeMapper, DefaultNotificationContextCodeMapper>()
            .AddScoped<INotificationContext, NotificationContext>();
    }

    public static IServiceCollection AddNotificationContext<TNotificationCodeMapper>(this IServiceCollection services)
        where TNotificationCodeMapper : INotificationContextCodeMapper, new()
    {
        return services
            .AddSingleton<INotificationContextCodeMapper>(new TNotificationCodeMapper())
            .AddScoped<INotificationContext, NotificationContext>();
    }
}