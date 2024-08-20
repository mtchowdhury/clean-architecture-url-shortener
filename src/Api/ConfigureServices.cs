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

        _ = services.AddHealthChecks();
        _ = services.AddAuthentication();
        _ = services.AddAuthorization();

        _ = services
            .AddAuthorization()
            .AddFastEndpoints();

        // Customise default API behaviour
        _ = services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}
