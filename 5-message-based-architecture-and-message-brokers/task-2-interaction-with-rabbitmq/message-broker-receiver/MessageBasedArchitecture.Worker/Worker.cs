using Domain.Events;
using MessageBasedArchitecture.Application;
using Newtonsoft.Json;

namespace MessageBasedArchitecture.Worker;

public class Worker : BackgroundService
{
    private readonly IMessageBroker messageBroker;
    private readonly ILogger<Worker> _logger;

    public Worker(IMessageBroker messageBroker, ILogger<Worker> logger)
    {
        this.messageBroker = messageBroker;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Run(() =>
            {
                var message = messageBroker.PullMessage<PriceChanged>();

                if (message == null)
                {
                    return;
                }

                _logger.LogInformation($"A new message has been received. Serialized message: {JsonConvert.SerializeObject(message)}");

                // TODO: Do something. The app should react to the message.
                // According to the homework the app should update something in the basket.
            });
        }
    }
}