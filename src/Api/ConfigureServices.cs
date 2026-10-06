using Api.Extensions;
using FastEndpoints.Security;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerService.Api.Middlewares;
using UrlShortenerService.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSingleton<ExceptionHandlingMiddleware>();

        _ = services.AddScoped<IUser, AuthHelper>();
        _ = services.AddSingleton<IBaseUrl, BaseUrlHelper>();

        _ = services.AddSingleton<IHashids>(
            new Hashids(
              salt: configuration["Hashids:Salt"],
              minHashLength: 6,
              alphabet: configuration["Hashids:Alphabet"])
            );
        _ = services.AddHttpContextAccessor();

        var authSettings = configuration.GetSection(AuthSettings.SectionName).Get<AuthSettings>() ?? new();
        if (string.IsNullOrWhiteSpace(authSettings.Secret) || authSettings.Secret.Length < 32)
            throw new InvalidOperationException("Auth:Secret must be configured and at least 32 characters long.");

        _ = services.Configure<AuthSettings>(configuration.GetSection(AuthSettings.SectionName));
        _ = services.AddSingleton<ICredentialValidator, ConfiguredCredentialValidator>();

        _ = services.AddHealthChecks();
        _ = services.AddAuthenticationJWTBearer(authSettings.Secret);

        _ = services
            .AddAuthorization()
            .AddFastEndpoints();

        // Customise default API behaviour
        _ = services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}
