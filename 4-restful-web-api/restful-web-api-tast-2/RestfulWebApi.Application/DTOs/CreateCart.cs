namespace RestfulWebApi.Application.DTOs;

public class CreateCart : BaseDto
{
    public IList<CreateCartItem>? Items { get; set; }
}