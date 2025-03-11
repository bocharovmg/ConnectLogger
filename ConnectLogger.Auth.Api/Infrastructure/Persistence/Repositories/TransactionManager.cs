using ConnectLogger.Auth.Api.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ConnectLogger.Auth.Api.Infrastructure.Persistence.Repositories;

public class TransactionManager(DbContext dbContext) : ITransactionManager
{
    private readonly DbContext _dbContext = dbContext;
    private bool _disposed;

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                _dbContext.Database.CurrentTransaction.Rollback();
                _dbContext.Database.CurrentTransaction.Dispose();
            }
        }

        _disposed = true;
    }

    ~TransactionManager()
    {
        Dispose(true);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction != null)
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction != null)
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction != null)
        {
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }
    }
}
