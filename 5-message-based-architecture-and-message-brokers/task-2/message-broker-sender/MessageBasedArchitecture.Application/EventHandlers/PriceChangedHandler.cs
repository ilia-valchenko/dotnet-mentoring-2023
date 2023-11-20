using MediatR;
using MessageBasedArchitecture.Domain.Events;

namespace MessageBasedArchitecture.Application.EventHandlers;

public class PriceChangedHandler : INotificationHandler<PriceChanged>
{
    public Task Handle(PriceChanged notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}