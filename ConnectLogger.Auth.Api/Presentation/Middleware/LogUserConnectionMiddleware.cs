using ConnectLogger.Auth.Api.Application.Extensions;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.GetAuthorizedUserId;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.LogUserConnection;


namespace ConnectLogger.Auth.Api.Presentation.Middleware;

public class LogUserConnectionMiddleware(
    IGetAuthorizedUserIdUseCase getAuthorizesUserUseCase,
    ILogUserConnectionUseCase logUserConnectionUseCase)
    : IMiddleware
{
    private readonly IGetAuthorizedUserIdUseCase _getAuthorizesUserUseCase = getAuthorizesUserUseCase;
    private readonly ILogUserConnectionUseCase _logUserConnectionUseCase = logUserConnectionUseCase;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await LogUserConnectionAsync(context);

        await next(context);
    }

    private async Task LogUserConnectionAsync(HttpContext context)
    {
        var userAddress = context.GetClientIpAddressString();

        var authorizedUserResponse = await _getAuthorizesUserUseCase.ExecuteAsync();
        if (authorizedUserResponse.IsSuccess && !string.IsNullOrWhiteSpace(userAddress))
        {
            var logUserConnectionRequest = new LogUserConnectionRequest
            {
                UserId = authorizedUserResponse.UserId!.Value,
                UserAddress = userAddress
            };
            _logUserConnectionUseCase.ExecuteAsync(logUserConnectionRequest).Wait();
        }
    }
}
