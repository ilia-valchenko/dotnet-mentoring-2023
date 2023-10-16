using Domain.Common;

namespace Domain.Entities;

public class Cart : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public IList<CartItem> Items { get; private set; } = new List<CartItem>();
    public int Quantity => this.Items.Count;
}