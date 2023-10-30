using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestfulWebApi.Infrastructure.Options;
using RestfulWebApi.Infrastructure.Repositories;
using RestfulWebApi.UseCase;
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestfulWebApi.Api", Version = "v1" });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<DataAccess>(Configuration.GetSection("DataAccess"));

            services.AddScoped<IRepository<Domain.Entities.Category>, CategoryRepository>();
            services.AddScoped<IRepository<Domain.Entities.Product>, ProductRepository>();
            services.AddScoped<IValidator<UseCase.DTOs.Category>, Validator<UseCase.DTOs.Category>>();
            services.AddScoped<IService<UseCase.DTOs.Category>, CategoryService>();
            services.AddScoped<IService<UseCase.DTOs.Product>, ProductService>();
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
