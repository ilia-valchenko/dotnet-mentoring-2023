//using ApiGateway.Web.Aggregators;
//using CorrelationId;
//using CorrelationId.DependencyInjection;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Ocelot.Cache.CacheManager;
//using Ocelot.DependencyInjection;
//using Ocelot.Middleware;

//var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("ocelot.json", false, true);

//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://localhost:7193/";
//        options.RequireHttpsMetadata = false;

//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = false,
//            ValidateIssuerSigningKey = false,
//            ValidIssuer = "https://localhost:7193",
//            ValidAudience = "API Gateway API resource"
//        };
//    });

//// The AddOcelot method adds default ASP.NET services to DI-container.
//// You could call another more extended AddOcelotUsingBuilder method while
//// configuring services to build and use custom builder via an IMvcCoreBuilder interface object.
//// See: https://ocelot.readthedocs.io/en/latest/features/dependencyinjection.html
//builder.Services
//    .AddOcelot()
//    .AddSingletonDefinedAggregator<ManufacturerProductAggregator>()
//    .AddCacheManager(x =>
//    {
//        x.WithDictionaryHandle();
//    });

//builder.Services.AddDefaultCorrelationId(cfg =>
//{
//    cfg.UpdateTraceIdentifier = true;
//    cfg.IncludeInResponse = true;
//    cfg.AddToLoggingScope = true;
//    cfg.RequestHeader = "X-Correlation-ID";
//    cfg.ResponseHeader = "X-Correlation-ID";
//});

//var app = builder.Build();

//if (builder.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

//app.UseCorrelationId();
//app.UseRouting();
//app.UseAuthorization();
//app.UseOcelot().Wait();

//app.Run();

using ApiGateway.Web;
using Microsoft.ApplicationInsights;
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
        //Log.Logger = new LoggerConfiguration()
        //    .WriteTo.Console() // You can add other sinks if needed.
        //    .WriteTo.ApplicationInsights(TelemetryConverter.Traces)
        //    .CreateLogger();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("ocelot.json", false, true)
                    .AddEnvironmentVariables();
            })
            .ConfigureLogging(logging =>
            {
                //// The ClearProviders method call is not necessarily required but can help maintain only those logging providers we want to use.
                //// So, clearing the default providers would remove Console and Debug logging providers which a production environment does not need.
                //logging.ClearProviders();

                //logging.AddSimpleConsole(options =>
                //{
                //    options.TimestampFormat = "'['yyyy'-'MM'-'dd HH':'mm':'ss']' ";
                //    options.UseUtcTimestamp = true;
                //    options.IncludeScopes = true;
                //    options.SingleLine = true;
                //});

                logging
                    .AddConsole()
                    .AddApplicationInsights();
            })
            //.UseSerilog()
            //.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration.WriteTo.Console())
            .UseSerilog((context, services, loggerConfiguration) =>
            {
                var telemetryClient = new TelemetryClient()
                {
                    InstrumentationKey = "cd542696-2486-4ccb-ba0c-98169d46a08a"
                };

                //loggerConfiguration
                //    .WriteTo.Console()
                //    .WriteTo.ApplicationInsights(
                //        services.GetRequiredService<TelemetryConfiguration>(),
                //        TelemetryConverter.Traces);

                loggerConfiguration
                    .WriteTo.Console()
                    .WriteTo.ApplicationInsights(telemetryClient, TelemetryConverter.Traces);

                // Setting writeToProviders parameter to true will enforce sending all log events to all registered logging providers,
                // even if there is no Serilog sink configured for a logging provider. It will cause duplicate log events when we have a sink configured.
                // 

            },
            writeToProviders: false)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    private static void SetupLoggerConfiguration(
        HostBuilderContext context,
        IServiceProvider provider,
        LoggerConfiguration loggerConfiguration)
    {
        var options = provider.GetRequiredService<IOptions<LogConfigOptions>>().Value;
        // LogConfigOptions is a home-brew configuration class
        // for high-level logging setup

        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationIdHeader("x-correlation-id")
            .Enrich.WithExceptionDetails();

        if (options.HttpRequestLogging)
        {
            // this line is required 'as is' in order
            // to enable HTTP request logging; see more here:
            // https://github.com/serilog/serilog-aspnetcore#request-logging
            loggerConfiguration.MinimumLevel.Override(
                "Microsoft.AspNetCore", LogEventLevel.Warning);
        }

        var readerOptions = new ConfigurationReaderOptions
        {
            SectionName = options.ConfigurationSectionName
        };
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration, readerOptions)
            .ReadFrom.Services(provider);

        // Configure Application Insights sink
        var telemetryConfiguration =
            provider.GetService<IOptions<TelemetryConfiguration>>();
        if (!string.IsNullOrEmpty(
            telemetryConfiguration?.Value.ConnectionString))
        {
            // We have a valid Application Insights setup
            loggerConfiguration
                .WriteTo
                .ApplicationInsights(
                    telemetryConfiguration.Value, TelemetryConverter.Traces);
        }

        // Configure Azure log stream sink
        if (options.LogToAzureFileSystem
            && !context.HostingEnvironment.IsDevelopment())
        {
            string path =
                $@"D:\home\LogFiles\Application\{context.HostingEnvironment.ApplicationName}.txt";
            loggerConfiguration
                .WriteTo
                .Async(x => x.File(
                    path, shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1)
                ));
        }
    }
}
