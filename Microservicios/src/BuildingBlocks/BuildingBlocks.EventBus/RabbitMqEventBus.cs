using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BuildingBlocks.EventBus;

public sealed class RabbitMqEventBus : IEventBus, IDisposable
{
    private readonly RabbitMqOptions _options;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly JsonSerializerOptions _jsonOptions;

    public RabbitMqEventBus(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;
        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            UserName = _options.UserName,
            Password = _options.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(_options.ExchangeName, ExchangeType.Topic, durable: true);

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public Task PublishAsync<T>(EventEnvelope<T> evt, CancellationToken ct = default)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(evt, _jsonOptions);
        var properties = _channel.CreateBasicProperties();
        properties.DeliveryMode = 2;

        _channel.BasicPublish(
            exchange: _options.ExchangeName,
            routingKey: evt.EventType,
            basicProperties: properties,
            body: body);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
        _channel.Dispose();
        _connection.Dispose();
    }
}
