using ConnectLogger.Auth.Api.Application.Interfaces.Services;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.LogUserConnection;
using ConnectLogger.Auth.Api.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace ConnectLogger.Auth.Api.Infrastructure.Configurations;

public static class AuthenticationConfiguration
{
    private const string AUTH_OPTIONS = "AuthOptions";
    private const string AUTHORIZATION_HEADER = "Authorization";

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = new AuthOptions();
        configuration.GetSection(AUTH_OPTIONS).Bind(authOptions);

        services
            .AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = !string.IsNullOrWhiteSpace(authOptions.Issuer),
                    ValidateAudience = !string.IsNullOrWhiteSpace(authOptions.Audience),
                    ValidateLifetime = authOptions.LifeTime.HasValue,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key))
                };
                options.SaveToken = true;

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = messageReceivedContext =>
                    {
                        var httpRequest = messageReceivedContext.Request;

                        if (!httpRequest.Headers.Keys.Contains(AUTHORIZATION_HEADER))
                        {
                            if (httpRequest.Cookies.ContainsKey(authOptions.CookieName))
                            {
                                var token = httpRequest.Cookies[authOptions.CookieName];
                                httpRequest.Headers.Append(AUTHORIZATION_HEADER, "Bearer " + token);
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
