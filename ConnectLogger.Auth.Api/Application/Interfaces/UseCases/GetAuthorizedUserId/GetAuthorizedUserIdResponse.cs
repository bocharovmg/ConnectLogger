namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases.GetAuthorizedUserId;

public class GetAuthorizedUserIdResponse : BaseResponse
{
    public long? UserId { get; set; }
}
