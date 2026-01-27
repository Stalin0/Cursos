using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BuildingBlocks.EventBus;

public class RabbitMqSubscriber<T> : BackgroundService
{
    private readonly RabbitMqOptions _options;
    private readonly IEventHandler<T> _handler;
    private readonly ServiceMetadata _metadata;
    private readonly string _eventType;
    private readonly ILogger<RabbitMqSubscriber<T>> _logger;
    private IConnection? _connection;
    private IModel? _channel;
    private readonly JsonSerializerOptions _jsonOptions;

    protected RabbitMqSubscriber(
        IOptions<RabbitMqOptions> options,
        IEventHandler<T> handler,
        ServiceMetadata metadata,
        string eventType,
        ILogger<RabbitMqSubscriber<T>> logger)
    {
        _options = options.Value;
        _handler = handler;
        _metadata = metadata;
        _eventType = eventType;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            UserName = _options.UserName,
            Password = _options.Password,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(_options.ExchangeName, ExchangeType.Topic, durable: true);

        var queueName = $"{_metadata.ServiceName}.{_eventType}".ToLowerInvariant();
        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: queueName, exchange: _options.ExchangeName, routingKey: _eventType);
        _channel.BasicQos(0, 1, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (_, args) =>
        {
            var payload = args.Body.ToArray();
            try
            {
                var evt = JsonSerializer.Deserialize<EventEnvelope<T>>(payload, _jsonOptions);
                if (evt is null)
                {
                    _logger.LogWarning("Null event payload for {EventType}", _eventType);
                    _channel.BasicAck(args.DeliveryTag, false);
                    return;
                }

                await _handler.HandleAsync(evt, stoppingToken);
                _channel.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling {EventType}", _eventType);
                _channel.BasicNack(args.DeliveryTag, false, requeue: true);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        return Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _channel?.Close();
        _connection?.Close();
        return base.StopAsync(stoppingToken);
    }
}
