using ConnectLogger.Auth.Api.Application.Enums;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases;


namespace ConnectLogger.Auth.Api.Presentation.Extensions;

public static class ResponseExtensions
{
    public static IResult GetHttpResult(this BaseResponse response)
    {
        if (!response.IsSuccess)
        {
            return response.ErrorType switch
            {
                ResponseErrorTypeEnum.AccessDenied => Results.Text(response.Message, statusCode: StatusCodes.Status401Unauthorized),
                ResponseErrorTypeEnum.InvalidProperties => Results.Conflict(BaseResponse.ConvertTo<BaseResponse>(response)),

                _ => Results.BadRequest(BaseResponse.ConvertTo<BaseResponse>(response))
            };
        }

        return Results.Ok(response);
    }
}
