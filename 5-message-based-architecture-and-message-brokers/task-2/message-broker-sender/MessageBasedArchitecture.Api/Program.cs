using MediatR;
using MessageBasedArchitecture.Application;
using MessageBasedArchitecture.Application.EventHandlers;
using MessageBasedArchitecture.Application.Options;
using MessageBasedArchitecture.Application.Services;
using MessageBasedArchitecture.Application.Services.Interfaces;
using MessageBasedArchitecture.Domain.Events;
using MessageBasedArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IMessageBroker, MessageBroker>();
builder.Services.AddSingleton<IRepository, InMemoryRepository>();
builder.Services.AddScoped<IService, Service>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PriceChanged).Assembly));

builder.Services.AddTransient<INotificationHandler<PriceChanged>, PriceChangedHandler>();

builder.Services.Configure<MessageBrokerConfiguration>(builder.Configuration.GetSection("MessageBrokerConfiguration"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
