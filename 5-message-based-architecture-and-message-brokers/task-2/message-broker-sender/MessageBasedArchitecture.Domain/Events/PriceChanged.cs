using MediatR;

namespace MessageBasedArchitecture.Domain.Events;

public class PriceChanged : INotification
{
    public Guid Id { get; set; }

    public double Price { get; set; }
}