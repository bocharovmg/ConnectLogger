using ConnectLogger.Connections.Api.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace ConnectLogger.Connections.Api.Infrastructure.Configurations;

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
