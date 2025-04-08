using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Shared.Serilog;

public static class HostBuilderExtensions
{
    public static void ConfigureLogging(HostBuilderContext hostBuilderContext, ILoggingBuilder logging)
    {
        var configuration = hostBuilderContext.Configuration;
        var envName = hostBuilderContext.HostingEnvironment.EnvironmentName;
        var appName = hostBuilderContext.HostingEnvironment.ApplicationName;

        CommonConfigueLogging(configuration, logging, envName, appName);
    }

    public static void ConfigureLogging(WebHostBuilderContext hostBuilderContext, ILoggingBuilder logging)
    {
        var configuration = hostBuilderContext.Configuration;
        var envName = hostBuilderContext.HostingEnvironment.EnvironmentName;
        var appName = hostBuilderContext.HostingEnvironment.ApplicationName;

        CommonConfigueLogging(configuration, logging, envName, appName);
    }


    private static void CommonConfigueLogging(IConfiguration configuration, ILoggingBuilder logging, string envName, string appName)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Environment", envName)
            .Enrich.WithProperty("ApplicationName", appName)
            .CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog();
    }
}
