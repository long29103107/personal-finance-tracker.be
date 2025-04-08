
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog.Context;
using Serilog;
using Serilog.Exceptions;

namespace Shared.Serilog;


/// <summary>
///    SerilogMiddleware is using to add new data for LogEntry
/// </summary>
public class SerilogMiddleware : IMiddleware
{
    private SerilogMiddlewareOptions _options;

    public SerilogMiddleware(IOptions<SerilogMiddlewareOptions> options)
    {
        _options = options?.Value;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_options != null)
        {
            if (_options.GenerateRCID)
            {
                context.Request.Headers["request-correlation-id"] = Guid.NewGuid().ToString();
            }
        }

        var requestId = context.Request.Headers["request-correlation-id"].ToString();
        using (LogContext.PushProperty("RCID", requestId))
        {
            await next.Invoke(context);
        }
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSerilogMiddleware(this IServiceCollection services)
    {
        services.AddScoped<SerilogMiddleware>();

        return services;
    }

    public static void ConfigureHostSerilogMiddleware(this IHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .CreateLogger();


        host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));
        host.ConfigureLogging(HostBuilderExtensions.ConfigureLogging);
    }

    public static IServiceCollection AddSerilogMiddleware(this IServiceCollection services, bool generateRCID)
    {
        services.Configure<SerilogMiddlewareOptions>(options =>
        {
            options.GenerateRCID = generateRCID;
        });
        AddSerilogMiddleware(services);
        return services;
    }
}

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSerilogMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<SerilogMiddleware>();

        return app;
    }
}

public class SerilogMiddlewareOptions
{
    public bool GenerateRCID { get; set; } = false;
}