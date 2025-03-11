using ConnectLogger.Auth.Api.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace ConnectLogger.Auth.Api.Infrastructure.Configurations;

public static class DbConfiguration
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<UserDbContext>(options =>
            {
                options
                    .UseNpgsql(configuration.GetConnectionString("AuthDB"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

        return services;
    }
}
