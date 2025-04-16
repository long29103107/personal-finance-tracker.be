using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Api.DependencyInjection.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // Enable JWT Bearer Authentication in Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Enter 'Bearer {your token}' in the field below. Example: 'Bearer abc123'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
                });


        builder.Host.AddAutofac();
        builder.Services.AddServiceLifetime();
        builder.Services.AddAuthenAuthorService(builder.Configuration);
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app, WebApplicationBuilder builder)
    {
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}



