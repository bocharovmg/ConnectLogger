using Confluent.Kafka;


namespace ConnectLogger.Auth.Api.Application.Options;

public class KafkaProducerOptions
{
    public string BootstrapServers { get; init; } = null!;

    public Acks? Acks { get; init; }

    public bool? EnableDeliveryReports { get; init; }

    public int? MessageTimeoutMs { get; init; }

    public int? RetryBackoffMs { get; init; }

    public int? MessageSendMaxRetries { get; init; }

    public int? BatchSize { get; init; }

    public double? LingerMs { get; init; }

    public CompressionType? CompressionType { get; init; }
}
