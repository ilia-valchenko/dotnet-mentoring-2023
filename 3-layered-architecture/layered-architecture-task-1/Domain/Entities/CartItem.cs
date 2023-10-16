using Domain.Common;

namespace Domain.Entities;

public class CartItem : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Image? Image { get; set; }
    public decimal Price { get; set; }
}