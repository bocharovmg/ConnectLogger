using Confluent.Kafka;
using ConnectLogger.Auth.Api.Application.Interfaces.Services;
using ConnectLogger.Auth.Api.Application.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;


namespace ConnectLogger.Auth.Api.Infrastructure.Persistence.Services;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly ILogger<KafkaProducerService> _logger;
    private readonly IProducer<string, string> _producer;
    private bool _disposed =  false;

    public KafkaProducerService(
        ILogger<KafkaProducerService> logger,
        IOptions<KafkaProducerOptions> producerOptions)
    {
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = producerOptions.Value.BootstrapServers,

            // Настройки надежности
            Acks = producerOptions.Value.Acks,
            EnableDeliveryReports = producerOptions.Value.EnableDeliveryReports,
            MessageTimeoutMs = producerOptions.Value.MessageTimeoutMs,
            RetryBackoffMs = producerOptions.Value.RetryBackoffMs,
            MessageSendMaxRetries = producerOptions.Value.MessageSendMaxRetries,

            // Настройки производительности
            BatchSize = producerOptions.Value.BatchSize,
            LingerMs = producerOptions.Value.LingerMs,
            CompressionType = producerOptions.Value.CompressionType,
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _producer?.Flush(TimeSpan.FromSeconds(10));
            _producer?.Dispose();
        }

        _disposed = true;
    }

    ~KafkaProducerService()
    {
        Dispose(true);
    }

    public async Task<DeliveryResult<string, string>> ProduceAsync<T>(string topic, T message, CancellationToken cancellationToken)
    {
        return await ProduceWithKeyAsync(topic, Guid.NewGuid().ToString(), message, cancellationToken);
    }

    public async Task<DeliveryResult<string, string>> ProduceWithKeyAsync<T>(string topic, string key, T message, CancellationToken cancellationToken)
    {
        try
        {
            var serializedMessage = JsonSerializer.Serialize(message);

            var kafkaMessage = new Message<string, string>
            {
                Key = key,
                Value = serializedMessage,
                Headers = new Headers
                    {
                        { "MessageType", System.Text.Encoding.UTF8.GetBytes(typeof(T).Name) },
                        { "Timestamp", System.Text.Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString("O")) }
                    }
            };

            var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage, cancellationToken);

            _logger.LogInformation(
                "Message sent successfully to topic {Topic}. Key: {Key}, Partition: {Partition}, Offset: {Offset}",
                topic,
                key,
                deliveryResult.Partition,
                deliveryResult.Offset
            );

            return deliveryResult;
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex,
                "Error producing message to topic {Topic}. Key: {Key}, Error: {Error}",
                topic,
                key,
                ex.Error.Reason
            );
            throw;
        }
    }
}
