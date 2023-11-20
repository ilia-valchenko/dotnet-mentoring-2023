using System.Text.Json;
using MediatR;
using MessageBasedArchitecture.Domain.Events;

namespace MessageBasedArchitecture.Application.EventHandlers;

public class PriceChangedHandler : INotificationHandler<PriceChanged>
{
    private readonly IMessageBroker _messageBroker;

    public PriceChangedHandler(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    public async Task Handle(PriceChanged notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        await Task.Run(() =>
        {
            var messageBody = JsonSerializer.SerializeToUtf8Bytes(notification);
            _messageBroker.PublishMessage(messageBody);
        });
    }
}