using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Catalog.Api.AuthorizationRequirements;

namespace Catalog.Api;

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

        // OpenID specifies how this communication and validation happen.
        // All the information we need will be discovered in the discovery document.
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", config => {
                // MetadataAddress is the discovery endpoint for obtaining metadata.
                // Example: .well-known/openid-configuration
                // config.MetadataAddress = "";

                // Now we need to tell our API where to pass our access token
                // in order to validate it.
                // In my case this is URL of my custom IdentityServer.
                config.Authority = "https://localhost:7193/";
                config.Audience = "catalog-api";
            });

        services.AddCors(confg =>
                confg.AddPolicy("AllowAll",
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.Api", Version = "v1" });

            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[]{}
                    }
                });
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddHttpClient()
            .AddHttpContextAccessor(); // It will allow us to get an access to the HttpContext.

        services.AddScoped<IAuthorizationHandler, JwtRequirementHandler>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.Api v1"));
        }

        app.UseCors("AllowAll");

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}