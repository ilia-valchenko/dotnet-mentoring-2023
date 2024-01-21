namespace MessageBasedArchitecture.Application.Options;

public class MessageBrokerConfiguration
{
    public string Uri { get; set; } = string.Empty;

    public string ClientProviderName { get; set; } = string.Empty;

    public string ExchangeName { get; set; } = string.Empty;

    public string RoutingKey { get; set; } = string.Empty;

    public string QueueName { get; set; } = string.Empty;
}