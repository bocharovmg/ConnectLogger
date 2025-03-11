using ConnectLogger.Connections.Logger.Consumer.Application.Options;
using ConnectLogger.Connections.Logger.Consumer.Infrastructure.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectLogger.Connections.Logger.Consumer.Infrastructure.Configurations;

public static class KafkaConfiguration
{
    public static IServiceCollection AddKafka(this IServiceCollection services)
    {
        return services
            .AddKafkaOptions()
            .AddHostedService<KafkaConsumerService>();
    }

    private static IServiceCollection AddKafkaOptions(this IServiceCollection services)
    {
        services.AddOptions<KafkaConsumerOptions>().BindConfiguration("Kafka");

        return services;
    }
}
