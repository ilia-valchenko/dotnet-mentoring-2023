namespace MessageBasedArchitecture.Application.Models;

public class Item
{
    public Guid Id { get ;set; }

    public string Name { get; set; } = string.Empty;

    public double Price { get; set; }
}