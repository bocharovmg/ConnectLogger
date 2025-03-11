using Confluent.Kafka;
using ConnectLogger.Connections.Logger.Consumer.Application.Entities;
using ConnectLogger.Connections.Logger.Consumer.Application.Extensions;
using ConnectLogger.Connections.Logger.Consumer.Application.Options;
using ConnectLogger.Connections.Logger.Consumer.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;


namespace ConnectLogger.Connections.Logger.Consumer.Infrastructure.Persistence.Services;

public class KafkaConsumerService : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConsumer<string, string> _consumer;
    private readonly string _topic;

    public KafkaConsumerService(
        ILogger<KafkaConsumerService> logger,
        IServiceProvider serviceProvider,
        IOptions<KafkaConsumerOptions> consumerOptions)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = consumerOptions.Value.BootstrapServers,
            GroupId = consumerOptions.Value.GroupId,
            ClientId = consumerOptions.Value.ClientId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
        };

        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        _topic = consumerOptions.Value.Topic;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            try
            {
                _consumer.Subscribe(_topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(stoppingToken);

                        if (consumeResult != null)
                        {
                            await ProcessMessageAsync(consumeResult, stoppingToken);
                            _consumer.Commit(consumeResult);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException)
                        {
                            return;
                        }

                        _logger.LogCritical(ex, ex.Message);
                    }
                }
            }
            finally
            {
                _consumer.Close();
                _consumer.Dispose();
            }
        }, stoppingToken);
    }

    private async Task ProcessMessageAsync(ConsumeResult<string, string> consumeResult, CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var connectLoggerDbContext = scope.ServiceProvider.GetRequiredService<ConnectLoggerDbContext>();

            var userConnectionLogMessage = JsonConvert.DeserializeObject<UserConnectionLogDto>(consumeResult.Message.Value)!;
            var ipAddress = IPAddress.Parse(userConnectionLogMessage.UserAddress);
            var ip4Address = ipAddress.MapToIPv4().ToString();
            var ip6Address = ipAddress.Get128BitString();

            var userConnectionLog = new UserConnectionLog
            {
                LogDateTime = DateTime.UtcNow,
                ConnectedUserId = userConnectionLogMessage.ConnectedUserId,
                UserIp4Address = ip4Address,
                UserIp6Address = ip6Address,
            };

            await connectLoggerDbContext.AddAsync(userConnectionLog, cancellationToken);
            await connectLoggerDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
            {
                throw;
            }

            _logger.LogCritical(ex, ex.Message);
        }
    }
}
