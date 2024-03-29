using ApiGateway.Web;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        Serilog.Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Serilog.Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
        }
        finally
        {
            Serilog.Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("ocelot.json", false, true)
                    .AddEnvironmentVariables();
            })
            //.ConfigureLogging(logging =>
            //{
            //    //// The ClearProviders method call is not necessarily required but can help maintain only those logging providers we want to use.
            //    //// So, clearing the default providers would remove Console and Debug logging providers which a production environment does not need.
            //    //logging.ClearProviders();

            //    //logging.AddSimpleConsole(options =>
            //    //{
            //    //    options.TimestampFormat = "'['yyyy'-'MM'-'dd HH':'mm':'ss']' ";
            //    //    options.UseUtcTimestamp = true;
            //    //    options.IncludeScopes = true;
            //    //    options.SingleLine = true;
            //    //});

            //    logging
            //        .AddConsole()
            //        .AddApplicationInsights();
            //})
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog(SetupLoggerConfiguration, writeToProviders: false);
    }

    private static void SetupLoggerConfiguration(
        HostBuilderContext context,
        IServiceProvider provider,
        LoggerConfiguration loggerConfiguration)
    {
        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationIdHeader("x-correlation-id")
            .Enrich.WithExceptionDetails();

        //// Note: read-only.
        //loggerConfiguration.MinimumLevel = LogEventLevel.Information;

        var readerOptions = new ConfigurationReaderOptions
        {
            SectionName = "Serilog"
        };

        // Serilog can be fully configured using its JSON configuration (an example of such configuration is provided with the sample project),
        // however we can use additional options (read from services). This will configure Serilog from the dependency container
        // and apply a registered LoggingLevelSwitch and all registered implementations of:
        // ILoggerSettings, IDestructuringPolicy, ILogEventEnricher, ILogEventFilter, and ILogEventSink.
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration, readerOptions)
            .ReadFrom.Services(provider);

        // Configure Application Insights sink
        var telemetryConfiguration = provider.GetService<IOptions<TelemetryConfiguration>>();

        if (!string.IsNullOrEmpty(telemetryConfiguration?.Value.ConnectionString))
        {
            // We have a valid Application Insights setup.
            // We have two converters out of the box - TelemetryConverter.Traces and TelemetryConverter.Events.
            // A telemetry converter is designed to format a log message and its parameters as a telemetry record for Application Insights.
            // Generally speaking, the trace converter creates Application Insights entries that are easier to read and analyze.
            // Note: When logging exceptions, the choice of converter does not matter, as a dedicated exception converter will always be used for exceptions.
            loggerConfiguration
                .WriteTo
                .ApplicationInsights(telemetryConfiguration.Value, TelemetryConverter.Traces);

            // There is the third optional parameter for the sink configuration: 'LogEventLevel restrictedToMinimumLevel'
            // which by default is set to LogEventLevel.Verbose, and which allows to skip any log event that has a lower level than this parameter.
            // Please note that this is different from the configured log level.
            // For example, Serilog may be configured to log all events with the level of Debug and higher,
            // but the sink may allow the events with the level of Warning and above.
            // This will cause the Application Insights sink to log only Warning level events, or higher,
            // while other sinks may submit Debug and Information log events.

            // And there is the fourth optional parameter: 'LoggingLevelSwitch levelSwitch' with the default value of null.
            // If specified, it allows to dynamically adjust allowed log levels for the sink.
        }
    }
}
