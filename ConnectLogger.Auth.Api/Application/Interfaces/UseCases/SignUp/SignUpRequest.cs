using Swashbuckle.AspNetCore.Annotations;


namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignUp;

/// <summary>
/// Модель запроса на создание пользователя
/// </summary>
[SwaggerSchema(Description = "Модель запроса на создание пользователя",
    Required = ["userName"])]
public class SignUpRequest
{
    [SwaggerSchema(Description = "Имя пользователя")]
    public string UserName { get; init; } = null!;
}
