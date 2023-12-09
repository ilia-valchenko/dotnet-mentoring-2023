using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestfulWebApi.Infrastructure.Options;
using RestfulWebApi.Infrastructure.Repositories;
using RestfulWebApi.UseCase.Repositories;
using RestfulWebApi.UseCase.Services;
using RestfulWebApi.UseCase.Services.Interfaces;
using RestfulWebApi.UseCase.Validators;
using RestfulWebApi.UseCase.Validators.Interfaces;

namespace RestfulWebApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // This authentication handler will automatically fetch the discovery document from IdentityServer on first use.
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", configureOptions =>
                {
                    //// The name of the API resource.
                    //options.Audience = "catalog-api";
                    //// The URL of my IdentityServer.
                    //options.Authority = "https://localhost:7193";

                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Constants.SecretKey);
                    var key = new SymmetricSecurityKey(secretBytes);

                    configureOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Constants.Constants.Issuer,
                        ValidAudience = Constants.Constants.Audience,
                        IssuerSigningKey = key
                    };
                });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestfulWebApi.Api", Version = "v1" });
            //});

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
}
