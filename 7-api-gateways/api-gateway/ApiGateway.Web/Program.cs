using System.Text;
using ApiGateway.Web.Aggregators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, true);
//builder.Configuration.AddJsonFile("ocelot.SwaggerEndPoints.json", false, true);

//builder.Services
//    .AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/ "Bearer")
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            //ValidateIssuer = true,
//            //ValidateAudience = true,
//            //ValidateLifetime = true,
//            //ValidateIssuerSigningKey = true,
//            //ValidIssuer = "https://localhost:7193", //builder.Configuration["JwtSettings:Issuer"],
//            //ValidAudience = "API Gateway API resource" //builder.Configuration["JwtSettings:Audience"],
//            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
//        };
//    });

builder.Services
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

//var routes = "Routes";

//builder.Configuration.AddOcelotWithSwaggerSupport(null, "/", "ocelot.SwaggerEndPoints");

// The AddOcelot method adds default ASP.NET services to DI-container.
// You could call another more extended AddOcelotUsingBuilder method while
// configuring services to build and use custom builder via an IMvcCoreBuilder interface object.
// See: https://ocelot.readthedocs.io/en/latest/features/dependencyinjection.html
builder.Services
    .AddOcelot()
    .AddSingletonDefinedAggregator<ManufacturerProductAggregator>();
//.AddTransientDefinedAggregator<ManufacturerProductAggregator>();

//builder.Services.AddSwaggerGen(option =>
//{
//    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });

//    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please enter a valid token",
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "Bearer"
//    });

//    option.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[]{}
//        }
//    });
//});

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway"));

//app.UseSwagger();
//app.UseSwaggerUI();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway"));

//app.UseSwaggerForOcelotUI(options =>
//{
//    options.PathToSwaggerGenerator = "/swagger/docs";
//});

app.UseRouting();
app.UseAuthorization();
app.UseOcelot().Wait();

app.Run();
