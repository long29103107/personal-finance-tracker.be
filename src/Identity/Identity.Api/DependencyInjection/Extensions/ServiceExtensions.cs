using Identity.Api.Services;
using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.DependencyInjection.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddAuthenAuthorService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Identity");

        services.AddDbContext<CustomIdentityDbContext>(
        (sp, options) =>
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(IdentityApiReference.AssemblyName))
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();

        });
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CustomIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddHttpContextAccessor();

        services.AddAuthentication(options =>
            {
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
                options.CallbackPath = "/auth/google/callback"; 
                options.Scope.Add("https://www.googleapis.com/auth/spreadsheets");
                options.Scope.Add("https://www.googleapis.com/auth/drive.file"); 
                options.SaveTokens = true;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        return services;
    }

    public static IServiceCollection AddServiceLifetime(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomAuthService, CustomAuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
