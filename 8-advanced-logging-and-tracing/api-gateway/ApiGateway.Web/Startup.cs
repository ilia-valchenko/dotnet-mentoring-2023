using ApiGateway.Web.Aggregators;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //Configuration.AddJsonFile("ocelot.json", false, true);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:7193/";
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = "https://localhost:7193",
                    ValidAudience = "API Gateway API resource"
                };
            });

        // The AddOcelot method adds default ASP.NET services to DI-container.
        // You could call another more extended AddOcelotUsingBuilder method while
        // configuring services to build and use custom builder via an IMvcCoreBuilder interface object.
        // See: https://ocelot.readthedocs.io/en/latest/features/dependencyinjection.html
        services
            .AddOcelot()
            .AddSingletonDefinedAggregator<ManufacturerProductAggregator>()
            .AddCacheManager(x =>
            {
                x.WithDictionaryHandle();
            });

        services.AddDefaultCorrelationId(cfg =>
        {
            cfg.UpdateTraceIdentifier = true;
            cfg.IncludeInResponse = true;
            cfg.AddToLoggingScope = true;
            cfg.RequestHeader = "X-Correlation-ID";
            cfg.ResponseHeader = "X-Correlation-ID";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseRouting();
        app.UseAuthorization();
        app.UseOcelot().Wait();
    }
}