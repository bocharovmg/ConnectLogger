using ConnectLogger.Connections.Logger.Consumer.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectLogger.Connections.Logger.Consumer.Infrastructure.Configurations;

public static class DbConfiguration
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ConnectLoggerDbContext>(options =>
            {
                options
                    .UseNpgsql(configuration.GetConnectionString("ConnectionLoggerDB"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

        return services;
    }
}
