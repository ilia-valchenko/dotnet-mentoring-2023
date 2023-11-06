namespace RestfulWebApi.Application.DTOs;

public class Cart : BaseDto
{
    public Guid Id { get; set; }

    public IList<CartItem> Items { get; set; } = new List<CartItem>();
}