using ConnectLogger.Auth.Api.Application.Interfaces.Repositories;
using ConnectLogger.Auth.Api.Infrastructure.Persistence.Repositories;

namespace ConnectLogger.Auth.Api.Infrastructure.Configurations;

public static class RepositoryConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserRepository, UserRepository>();
    }
}
