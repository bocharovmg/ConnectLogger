namespace ConnectLogger.Connections.Logger.Consumer.Application.Options;

public class KafkaConsumerOptions
{
    public string BootstrapServers { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string Topic { get; set; } = null!;
}
