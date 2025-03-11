using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.GetAuthorizedUserId;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.LogUserConnection;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignIn;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignOut;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignUp;
using ConnectLogger.Auth.Api.Application.Options;
using ConnectLogger.Auth.Api.Application.UseCases.User;
using ConnectLogger.Auth.Api.Application.UseCases.LogUserConnection;


namespace ConnectLogger.Auth.Api.Infrastructure.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services
            .AddUseCaseOptions()
            .AddScoped<IGetAuthorizedUserIdUseCase, GetAuthorizedUserIdUseCase>()
            .AddScoped<ILogUserConnectionUseCase, LogUserConnectionUseCase>()
            .AddScoped<ISignUpUseCase, SignUpUseCase>()
            .AddScoped<ISignInUseCase, SignInUseCase>()
            .AddScoped<ISignOutUseCase, SignOutUseCase>();
    }

    private static IServiceCollection AddUseCaseOptions(this IServiceCollection services)
    {
        services.AddOptions<KafkaTopicOptions>().BindConfiguration("Kafka");
        services.AddOptions<AuthOptions>().BindConfiguration("AuthOptions");
        services.AddOptions<CookieOptions>().BindConfiguration("CookieOptions");

        return services;
    }
}
