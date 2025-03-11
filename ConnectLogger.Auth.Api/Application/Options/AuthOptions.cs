namespace ConnectLogger.Auth.Api.Application.Options;

public class AuthOptions
{
    public string CookieName { get; init; } = null!;

    public string Issuer { get; init; } = null!;

    public string Audience { get; init; } = null!;

    public string Key { get; init; } = null!;

    public int? LifeTime { get; init; }
}
