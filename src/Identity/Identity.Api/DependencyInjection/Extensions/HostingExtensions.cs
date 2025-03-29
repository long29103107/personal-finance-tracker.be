using Identity.Api.Services.Abstractions;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace Identity.Api.DependencyInjection.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? string.Empty;
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
                options.CallbackPath = "/auth/google/callback"; // Đảm bảo giống trên Google Cloud
                options.SaveTokens = true; // Lưu token để sử dụng sau
            });
        builder.Services.AddAuthorization();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("LonG", policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app, WebApplicationBuilder builder)
    {
        app.UseCors("LonG");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}



