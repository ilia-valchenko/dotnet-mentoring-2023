namespace ApiGateway.Web.DTOs;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ImageUrlText { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public Guid ManufacturerId { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }
}