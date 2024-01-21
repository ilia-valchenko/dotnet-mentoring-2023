namespace Domain.Events;

public class PriceChanged : BaseEvent
{
    public Guid Id { get; set; }

    public double Price { get; set; }
}