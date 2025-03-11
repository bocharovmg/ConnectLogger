namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases.LogUserConnection;

/// <summary>
/// Модель запроса на сохранение в логах информации о подключении пользователя
/// </summary>
public class LogUserConnectionRequest
{
    public long UserId { get; init; }

    public string UserAddress { get; init; } = null!;
}
