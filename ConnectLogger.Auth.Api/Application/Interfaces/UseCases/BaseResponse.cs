using ConnectLogger.Auth.Api.Application.Enums;
using System.Text.Json.Serialization;


namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases;

public class BaseResponse
{
    [JsonIgnore]
    public bool IsSuccess { get; set; }

    [JsonIgnore]
    public ResponseErrorTypeEnum? ErrorType { get; set; }

    public string? Message { get; set; }

    public static BaseResponse Error(string? message, ResponseErrorTypeEnum? errorType = null)
    {
        return new BaseResponse()
        {
            IsSuccess = false,
            Message = message,
            ErrorType = errorType,
        };
    }

    public static T Error<T>(string? message, ResponseErrorTypeEnum? errorType = null) where T : BaseResponse, new()
    {
        return new T()
        {
            IsSuccess = false,
            Message = message,
            ErrorType = errorType,
        };
    }

    public static TResponse ConvertTo<TResponse>(BaseResponse response) where TResponse : BaseResponse, new()
    {
        var result = new TResponse
        {
            IsSuccess = response.IsSuccess,
            ErrorType = response.ErrorType,
            Message = response.Message,
        };

        return result;
    }
}
