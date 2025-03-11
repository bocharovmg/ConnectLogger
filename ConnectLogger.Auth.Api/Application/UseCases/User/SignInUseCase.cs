using ConnectLogger.Auth.Api.Application.Dtos;
using ConnectLogger.Auth.Api.Application.Entities;
using ConnectLogger.Auth.Api.Application.Enums;
using ConnectLogger.Auth.Api.Application.Extensions;
using ConnectLogger.Auth.Api.Application.Interfaces.Repositories;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.LogUserConnection;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignIn;
using ConnectLogger.Auth.Api.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ConnectLogger.Auth.Api.Application.UseCases.User;

public class SignInUseCase(
    IOptions<AuthOptions> authOptions,
    IOptions<CookieOptions> cookiesOptions,
    IHttpContextAccessor http,
    IUserRepository userRepository,
    ILogUserConnectionUseCase logUserConnectionUseCase) : ISignInUseCase
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    private readonly CookieOptions _cookiesOptions = cookiesOptions.Value;
    private readonly IHttpContextAccessor _http = http;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogUserConnectionUseCase _logUserConnectionUseCase = logUserConnectionUseCase;

    public async Task<SignInResponse> ExecuteAsync(SignInRequest request, CancellationToken cancellationToken = default)
    {
        var httpContext = _http.HttpContext!;

        var user = await _userRepository.GetUserByUserNameAsync(request.UserName, cancellationToken);
        if (user == null)
        {
            return BaseResponse
                .Error<SignInResponse>(
                    "Пользователь с таким учетными данными отсутствует",
                    ResponseErrorTypeEnum.AccessDenied);
        }

        var token = CreateToken(user);
        httpContext.Response.Cookies.Append(_authOptions.CookieName, token, _cookiesOptions);

        var userAddress = httpContext.GetClientIpAddressString();
        if (userAddress != null)
        {
            var logUserConnectionRequest = new LogUserConnectionRequest
            {
                UserId = user.UserId,
                UserAddress = userAddress
            };
            await _logUserConnectionUseCase.ExecuteAsync(logUserConnectionRequest, cancellationToken);
        }

        return new SignInResponse
        {
            IsSuccess = true,
            User = new UserDto
            {
                UserId = user.UserId,
            }
        };
    }

    private string CreateToken(Entities.User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(nameof(Entities.User.UserId), user.UserId.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: claims,
            expires: _authOptions.LifeTime.HasValue ? DateTime.UtcNow.AddMinutes(_authOptions.LifeTime.Value) : null,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
