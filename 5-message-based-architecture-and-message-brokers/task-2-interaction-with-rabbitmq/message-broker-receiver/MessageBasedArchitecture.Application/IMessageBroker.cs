namespace MessageBasedArchitecture.Application;

public interface IMessageBroker
{
    T? PullMessage<T>() where T : class;
}