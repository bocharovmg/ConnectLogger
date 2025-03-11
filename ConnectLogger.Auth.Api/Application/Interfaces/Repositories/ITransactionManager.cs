namespace ConnectLogger.Auth.Api.Application.Interfaces.Repositories;

public interface ITransactionManager : IDisposable
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
