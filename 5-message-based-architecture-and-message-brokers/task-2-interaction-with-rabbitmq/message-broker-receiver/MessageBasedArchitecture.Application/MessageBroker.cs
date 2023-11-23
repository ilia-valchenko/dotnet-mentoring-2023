using System.Text.Json;
using MessageBasedArchitecture.Application.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBasedArchitecture.Application;

public class MessageBroker : IMessageBroker
{
    private readonly MessageBrokerConfiguration _messageBrokerConfiguration;

    public MessageBroker(IOptions<MessageBrokerConfiguration> MessageBrokerOptions)
    {
        _messageBrokerConfiguration = MessageBrokerOptions.Value;
    }

    public T? PullMessage<T>() where T : class
    {
        using var connection = this.CreateConnection();
        using var channel = this.CreateChannel(connection);
        var getResult = channel.BasicGet(_messageBrokerConfiguration.QueueName, true);

        if (getResult == null || getResult.Body.IsEmpty)
        {
            return null;
        }

        var data = getResult.Body.ToArray();
        return JsonSerializer.Deserialize<T>(data);
    }

    private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        throw new NotImplementedException();
    }

    private IConnection CreateConnection()
    {
        var connectionFactory = new ConnectionFactory();
        connectionFactory.Uri = new Uri(_messageBrokerConfiguration.Uri);
        connectionFactory.ClientProvidedName = _messageBrokerConfiguration.ClientProviderName;

        return connectionFactory.CreateConnection();
    }

    private IModel CreateChannel(IConnection connection)
    {
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(_messageBrokerConfiguration.ExchangeName, ExchangeType.Direct);
        channel.QueueDeclare(_messageBrokerConfiguration.QueueName, false, false, false, null);

        channel.QueueBind(
            _messageBrokerConfiguration.QueueName,
            _messageBrokerConfiguration.ExchangeName,
            _messageBrokerConfiguration.RoutingKey,
            null);

        channel.BasicQos(
            prefetchSize: 0, // We don't care about message size.
            prefetchCount: 1, // The number of messages we want to pull at once.
            global: false); // False means that we want to apply it to the current instance. Not to entire system.

        return channel;
    }
}
