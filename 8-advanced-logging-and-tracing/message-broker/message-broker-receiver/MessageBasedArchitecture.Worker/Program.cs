using MessageBasedArchitecture.Application;
using MessageBasedArchitecture.Application.Options;
using MessageBasedArchitecture.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<MessageBrokerConfiguration>(hostContext.Configuration.GetSection("MessageBrokerConfiguration"));
        services.AddHostedService<Worker>();
        services.AddSingleton<IMessageBroker, MessageBroker>();
    })
    .Build();

await host.RunAsync();
