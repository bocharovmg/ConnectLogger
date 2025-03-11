using ConnectLogger.Auth.Api.Application.Dtos;


namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignIn;

public class SignInResponse : BaseResponse
{
    public UserDto? User { get; set; }
}
