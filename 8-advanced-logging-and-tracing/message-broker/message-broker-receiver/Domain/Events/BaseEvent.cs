namespace Domain.Events;

public class BaseEvent
{
    public Guid CorrelationId { get; set; }
}