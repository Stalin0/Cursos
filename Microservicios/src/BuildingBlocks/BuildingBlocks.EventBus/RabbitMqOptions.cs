namespace BuildingBlocks.EventBus;

public class RabbitMqOptions
{
    public string HostName { get; init; } = "localhost";
    public string UserName { get; init; } = "guest";
    public string Password { get; init; } = "guest";
    public string ExchangeName { get; init; } = "microservices.events";
}
