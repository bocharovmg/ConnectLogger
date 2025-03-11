using Confluent.Kafka;


namespace ConnectLogger.Auth.Api.Application.Interfaces.Services;

public interface IKafkaProducerService : IDisposable
{
    /// <summary>
    /// Публикация сообщения в указанный топик
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="topic"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<DeliveryResult<string, string>> ProduceAsync<T>(string topic, T message, CancellationToken cancellationToken);

    /// <summary>
    /// Публикация сообщения в указанный топик с ключем
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="topic"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<DeliveryResult<string, string>> ProduceWithKeyAsync<T>(string topic, string key, T message, CancellationToken cancellationToken);
}
