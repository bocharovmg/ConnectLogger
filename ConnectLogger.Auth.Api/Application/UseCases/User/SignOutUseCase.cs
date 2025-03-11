using ConnectLogger.Auth.Api.Application.Interfaces.UseCases;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignOut;
using ConnectLogger.Auth.Api.Application.Options;
using Microsoft.Extensions.Options;


namespace ConnectLogger.Auth.Api.Application.UseCases.User;

public class SignOutUseCase(
    IOptions<AuthOptions> authOptions,
    IOptions<CookieOptions> cookiesOptions,
    IHttpContextAccessor http) : ISignOutUseCase
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    private readonly CookieOptions _cookiesOptions = cookiesOptions.Value;
    private readonly IHttpContextAccessor _http = http;

    public Task<SignOutResponse> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var httpContext = _http.HttpContext!;

        httpContext.Response.Cookies.Delete(_authOptions.CookieName, _cookiesOptions);

        return Task.FromResult(new SignOutResponse
        {
            IsSuccess = true,
        });
    }
}
