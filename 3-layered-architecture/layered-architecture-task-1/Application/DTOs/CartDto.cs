namespace Application.DTOs;

public class CartDto
{
    public Guid Id { get; set; }
    public IList<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public int Quantity => this.Items.Count;
}
