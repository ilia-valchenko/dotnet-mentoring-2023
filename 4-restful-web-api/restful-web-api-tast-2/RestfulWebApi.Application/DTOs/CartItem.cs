namespace RestfulWebApi.Application.DTOs;

public class CartItem : BaseDto
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }
}