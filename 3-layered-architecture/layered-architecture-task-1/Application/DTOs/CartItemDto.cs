using Domain.ValueObjects;

namespace Application.DTOs;

public class CartItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Image? Image { get; set; }
    public decimal Price { get; set; }
}
