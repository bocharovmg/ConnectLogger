using ConnectLogger.Auth.Api.Application.Interfaces.Services;
using ConnectLogger.Auth.Api.Application.Options;
using ConnectLogger.Auth.Api.Infrastructure.Persistence.Services;


namespace ConnectLogger.Auth.Api.Infrastructure.Configurations;

public static class KafkaConfiguration
{
    public static IServiceCollection AddKafka(this IServiceCollection services)
    {
        return services
            .AddKafkaOptions()
            .AddSingleton<IKafkaProducerService, KafkaProducerService>();
    }

    private static IServiceCollection AddKafkaOptions(this IServiceCollection services)
    {
        services.AddOptions<KafkaProducerOptions>().BindConfiguration("Kafka:Options");

        return services;
    }
}
