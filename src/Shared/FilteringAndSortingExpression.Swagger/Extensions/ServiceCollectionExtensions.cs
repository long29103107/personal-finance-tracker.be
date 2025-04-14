using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using static FilteringAndSortingExpression.Swagger.Helpers.Helper;

namespace FilteringAndSortingExpression.Swagger.Extensions;

public static class ServiceCollectionExtensions
{
    public static SwaggerConfig SwaggerConfig;

    public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerConfig> action = null)
    {
        SwaggerConfig = new SwaggerConfig()
        {
            Name = Assembly.GetEntryAssembly().GetName().Name,
            Version = $"v{Assembly.GetEntryAssembly().GetName().Version.Major}",
            Title = Assembly.GetEntryAssembly().GetName().Name,
        };

        action(SwaggerConfig);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(SwaggerConfig.Version, new OpenApiInfo
            {
                Title = SwaggerConfig.Title,
                Version = SwaggerConfig.Version,
                Description = SwaggerConfig.Description
            });

            c.EnableAnnotations();

            // Add authentication
            if (SwaggerConfig.EnableAuthentioncation)
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            }

            c.OperationFilter<SwaggerExcludeFilter>();
            c.SchemaFilter<SwaggerIgnoreFilter>();
        });


        //services.AddSwaggerGenNewtonsoftSupport();
        return services;
    }

}