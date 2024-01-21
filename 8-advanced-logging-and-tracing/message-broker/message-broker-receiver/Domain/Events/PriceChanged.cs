namespace Domain.Events;

public class PriceChanged
{
    public Guid Id { get; set; }

    public double Price { get; set; }
}