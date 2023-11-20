using MessageBasedArchitecture.Application.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MessageBasedArchitecture.Application;

public class MessageBroker : IMessageBroker
{
    private readonly MessageBrokerConfiguration _messageBrokerConfiguration;

    public MessageBroker(IOptions<MessageBrokerConfiguration> MessageBrokerOptions)
    {
        _messageBrokerConfiguration = MessageBrokerOptions.Value;
    }

    public void PublishMessage(byte[] messageBody)
    {
        if (messageBody == null)
        {
            throw new ArgumentNullException(nameof(messageBody));
        }

        if (messageBody.Length == 0)
        {
            throw new ArgumentException("The message body is empty.");
        }

        var connectionFactory = new ConnectionFactory();
        connectionFactory.Uri = new Uri(_messageBrokerConfiguration.Uri);
        connectionFactory.ClientProvidedName = _messageBrokerConfiguration.ClientProviderName;

        using var connection = connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(_messageBrokerConfiguration.ExchangeName, ExchangeType.Direct);
        channel.QueueDeclare(_messageBrokerConfiguration.QueueName, false, false, false, null);

        channel.QueueBind(
            _messageBrokerConfiguration.QueueName,
            _messageBrokerConfiguration.ExchangeName,
            _messageBrokerConfiguration.RoutingKey,
            null);

        channel.BasicPublish(
            _messageBrokerConfiguration.ExchangeName,
            _messageBrokerConfiguration.RoutingKey,
            null,
            messageBody);

        channel.Close();
        connection.Close();
    }
}