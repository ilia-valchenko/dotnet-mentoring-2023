using Domain.Events;
using MessageBasedArchitecture.Application;
using Newtonsoft.Json;
using Serilog.Context;

namespace MessageBasedArchitecture.Worker;

public class Worker : BackgroundService
{
    private readonly IMessageBroker messageBroker;

    public Worker(IMessageBroker messageBroker)
    {
        this.messageBroker = messageBroker;
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

                using (LogContext.PushProperty("CorrelationId", message.CorrelationId.ToString()))
                {
                    Serilog.Log.Information(
                        $"[MessageBasedArchitecture Worker] A new message has been received. Serialized message: {JsonConvert.SerializeObject(message)}.");

                    // TODO: Do something. The app should react to the message.
                    // According to the homework the app should update something in the basket.
                }
            });
        }
    }
}