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
using RestfulWebApi.Api.AuthorizationRequirements;
using RestfulWebApi.Infrastructure.Options;
using RestfulWebApi.Infrastructure.Repositories;
using RestfulWebApi.UseCase.Repositories;
using RestfulWebApi.UseCase.Services;
using RestfulWebApi.UseCase.Services.Interfaces;
using RestfulWebApi.UseCase.Validators;
using RestfulWebApi.UseCase.Validators.Interfaces;

namespace RestfulWebApi.Api;

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

        #region DummyIdentityServer configuration

        //// This authentication handler will automatically fetch the discovery document from IdentityServer on first use.
        //services.AddAuthentication("Bearer")
        //    .AddJwtBearer("Bearer", configureOptions =>
        //    {
        //        var secretBytes = Encoding.UTF8.GetBytes(Constants.Constants.SecretKey);
        //        var key = new SymmetricSecurityKey(secretBytes);

        //        // The Resource Server MUST validate an access token
        //        // and ensure that is has not expired and that its scope
        //        // covers the requested resource. It generally involves
        //        // an interaction or coordination between the Resource Server
        //        // and Authorization Server.
        //        configureOptions.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidIssuer = Constants.Constants.Issuer,
        //            ValidAudience = Constants.Constants.Audience,
        //            IssuerSigningKey = key
        //        };
        //    });

        //services.AddAuthorization(config =>
        //{
        //    var defaultAuthBuilder = new AuthorizationPolicyBuilder();

        //    //var defaultAuthPolicy = defaultAuthBuilder
        //    //    .RequireAuthenticatedUser()
        //    //    .RequireClaim(ClaimTypes.DateOfBirth)
        //    //    .Build();

        //    // Here we override the default policy.
        //    var defaultAuthPolicy = defaultAuthBuilder
        //        // Here we usually specify requirements for which we are going
        //        // to use handlers to validate.
        //        .AddRequirements(new JwtRequirement())
        //        // For default authorization we're gonna redirect it to the server.
        //        .Build();

        //    config.DefaultPolicy = defaultAuthPolicy;

        //    // ****************************************************************

        //    //config.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "admin"));

        //    //config.AddPolicy("Claim.DoB", policyBuilder =>
        //    //{
        //    //    policyBuilder.AddRequireCustomClaimRequirement(ClaimTypes.DateOfBirth);
        //    //});
        //});

        #endregion

        #region IdentityServer4 configuration

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

        #endregion

        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "RestfulWebApi.Api", Version = "v1" });

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

        services.Configure<DataAccess>(Configuration.GetSection("DataAccess"));

        services.AddHttpClient()
            .AddHttpContextAccessor(); // It will allow us to get an access to the HttpContext.

        services.AddScoped<IAuthorizationHandler, JwtRequirementHandler>();

        services.AddScoped<IRepository<Domain.Entities.Category>, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IValidator<UseCase.DTOs.BaseDto>, Validator<UseCase.DTOs.BaseDto>>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestfulWebApi.Api v1"));
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}