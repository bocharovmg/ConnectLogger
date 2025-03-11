using Swashbuckle.AspNetCore.Annotations;


namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignIn;

/// <summary>
/// Модель запроса пользователя по имени пользователя
/// </summary>
[SwaggerSchema(Description = "Модель запроса пользователя по имени пользователя",
    Required = ["userName"])]
public class SignInRequest
{
    [SwaggerSchema(Description = "Имя пользователя")]
    public string UserName { get; init; } = null!;
}
