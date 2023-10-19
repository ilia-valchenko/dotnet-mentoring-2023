using Application.Common.Interfaces;
using Application.DTOs;
using Console;

System.Console.WriteLine("-------------- START --------------");

ICartServiceFactory cartServiceFactory = new CartServiceFactory();
ICartService cartService = cartServiceFactory.CreateCartService();

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
    await cartService.AddItemAsync(cart.Id, itemToAdd);
}
catch (Exception ex)
{
    System.Console.WriteLine($"Something went wrong. Exception: {ex.ToString()}");
}

System.Console.WriteLine("\n\nTap to continue...");
System.Console.ReadKey();
