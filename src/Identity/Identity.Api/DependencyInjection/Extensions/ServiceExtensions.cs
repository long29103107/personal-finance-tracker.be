using Identity.Api.Services;
using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace Identity.Api.DependencyInjection.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddAuthenAuthorService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
                options.CallbackPath = "/auth/google/callback"; // Đảm bảo giống trên Google Cloud
                options.SaveTokens = true; // Lưu token để sử dụng sau
            });
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddServiceLifetime(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
