using Application;
using Application.DTOs;
using Application.EventHandlers;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Console;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

System.Console.WriteLine("-------------- START --------------");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CartItemAddedEventHandler).Assembly)))
    .Build();

using (var serviceScope = host.Services.CreateScope())
{
    string connectionString = "Data Source=../../../../AppData/catalog-service-database.db;";
    IMediator mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    IMapperConfigurationFactory mapperConfigurationFactory = new MapperConfigurationFactory();
    IMapperFactory mapperFactory = new MapperFactory(mapperConfigurationFactory);
    IMapper mapper = mapperFactory.CreateMapper();
    ICartRepository cartRepository = new CartRepository(connectionString);
    ICartService cartService = new CartService(cartRepository, mapper);
    ICartItemService cartItemService = new CartItemService(cartRepository, mediator, mapper);

    var cart = new CartDto
    {
        Id = Guid.NewGuid()
    };

    var itemToAdd = new CartItemDto
    {
        Id = Guid.NewGuid(),
        Name = "TestItem1",
        Description = "TestDescriptionForTestItem1"
    };

    try
    {
        await cartService.CreateAsync(cart);
        await cartItemService.AddItemAsync(cart.Id, itemToAdd);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"Something went wrong. Exception: {ex.ToString()}");
    }

    System.Console.WriteLine("\n\nTap to continue...");
    System.Console.ReadKey();
}
