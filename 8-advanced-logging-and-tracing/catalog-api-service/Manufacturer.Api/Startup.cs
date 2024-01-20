using Microsoft.OpenApi.Models;
using Serilog;

namespace Manufacturer.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddCors(confg =>
                confg.AddPolicy("AllowAll",
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Manufacturer.Api", Version = "v1" });
        });

        //services.AddDefaultCorrelationId(cfg =>
        //{
        //    cfg.UpdateTraceIdentifier = true;
        //    cfg.IncludeInResponse = true;
        //    cfg.AddToLoggingScope = true;
        //    cfg.RequestHeader = "X-Correlation-ID";
        //    cfg.ResponseHeader = "X-Correlation-ID";
        //});

        // IMPORTANT!
        // It builds a TelemetryConfiguration instance with the connection string we configured.
        // The method also registers a singleton of IOptions<TelemetryConfiguration> that can be resolved from an IServiceProvider.
        services.AddApplicationInsightsTelemetry();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manufacturer.Api v1"));
        }

        app.UseCors("AllowAll");

        //app.UseCorrelationId();

        // This method will register the request logging middleware, and thus should be called early,
        // before registering other middleware and handlers such as MVC, otherwise it will not be able to log them.
        app.UseSerilogRequestLogging();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
