using Fwks.AspNetCore.Extensions;
using Fwks.Core.Extensions;
using Fwks.ExampleService.App.Api.Configuration;
using Fwks.ExampleService.Core.Constants;
using Fwks.ExampleService.Core.Settings;
using Fwks.Security.Obfuscation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;

namespace Fwks.ExampleService.App.Api.Configuration;

internal static class SecurityConfiguration
{
    internal static IServiceCollection AddSecurity(this IServiceCollection services, AppSettings appSettings)
    {
        Obfuscator.Setup(appSettings.Security.ObfuscationKey, 7);

        return services
            .AddAuthServer(appSettings)
            .AddCors(appSettings);
    }

    internal static IApplicationBuilder UseSecurity(this IApplicationBuilder app)
    {
        return app
            .UseCors(CorsConfiguration.PolicyName)
            .UseAuthentication()
            .UseAuthorization();
    }

    private static IServiceCollection AddAuthServer(this IServiceCollection services, AppSettings appSettings)
    {
        if (appSettings.Security.AuthServer.Authority.IsEmpty())
            return services;

        IdentityModelEventSource.HeaderWritten = false;

        return services
            .AddAuthorizationCore()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                var logger = services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

                x.Authority = appSettings.Security.AuthServer.Authority;
                x.Audience = appSettings.Security.AuthServer.Audience;
                x.RequireHttpsMetadata = appSettings.Security.AuthServer.RequireHttpsMetadata;
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = JwtBearerEventsExtensions.OnAuthenticationFailed(logger),
                    OnForbidden = JwtBearerEventsExtensions.OnForbidden(logger)
                };
            })
            .Services;
    }

    private static IServiceCollection AddCors(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddCors(x =>
                x.AddPolicy(CorsConfiguration.PolicyName, policy =>
                    policy
                        .WithOrigins(appSettings.Security.Cors.AllowedOrigins)
                        .WithHeaders(appSettings.Security.Cors.AllowedHeaders)
                        .WithMethods(CorsConfiguration.AllowedMethods)));
    }
}