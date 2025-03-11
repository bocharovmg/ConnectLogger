using ConnectLogger.Auth.Api.Application.Dtos;


namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignUp;

public class SignUpResponse : BaseResponse
{
    public UserDto? User { get; set; }
}
