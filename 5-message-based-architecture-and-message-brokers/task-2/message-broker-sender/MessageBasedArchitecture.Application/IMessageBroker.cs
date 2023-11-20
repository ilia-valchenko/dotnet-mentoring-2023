namespace MessageBasedArchitecture.Application;

public interface IMessageBroker
{
    void PublishMessage(byte[] messageBody);
}
