namespace ConnectLogger.Auth.Api.Application.Interfaces.UseCases;

public interface IUseCase<TResponse> where TResponse : BaseResponse
{
    Task<TResponse> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IUseCase<TRequest, TResponse> where TResponse : BaseResponse
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}
