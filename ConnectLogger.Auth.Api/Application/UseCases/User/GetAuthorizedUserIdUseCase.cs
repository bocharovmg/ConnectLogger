using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.GetAuthorizedUserId;
using ConnectLogger.Auth.Api.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace ConnectLogger.Auth.Api.Application.UseCases.User;

public class GetAuthorizedUserIdUseCase(
    IOptions<AuthOptions> authOptions,
    IHttpContextAccessor http)
    : IGetAuthorizedUserIdUseCase
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    private readonly IHttpContextAccessor _http = http;

    public Task<GetAuthorizedUserIdResponse> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var token = _http.HttpContext!.Request.Cookies[_authOptions.CookieName];
        if (string.IsNullOrEmpty(token))
        {
            return Task.FromResult(new GetAuthorizedUserIdResponse
            {
                IsSuccess = false,
            });
        }

        var jwtHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtHandler.ReadJwtToken(token);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrWhiteSpace(_authOptions.Issuer),
            ValidateAudience = !string.IsNullOrWhiteSpace(_authOptions.Audience),
            ValidateLifetime = _authOptions.LifeTime.HasValue,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _authOptions.Issuer,
            ValidAudience = _authOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key))
        };

        try
        {
            var validationResult = jwtHandler.ValidateToken(token, validationParameters, out var _);

            var userIdClaim = validationResult.Claims
                .First(claim => claim.Type == nameof(Entities.User.UserId));
            return Task.FromResult(new GetAuthorizedUserIdResponse
            {
                IsSuccess = true,
                UserId = Convert.ToInt32(userIdClaim.Value)
            });
        }
        catch (Exception ex)
        {
            if (!(ex is SecurityTokenValidationException))
            {
                throw;
            }
        }

        return Task.FromResult(new GetAuthorizedUserIdResponse
        {
            IsSuccess = false,
        });
    }
}
