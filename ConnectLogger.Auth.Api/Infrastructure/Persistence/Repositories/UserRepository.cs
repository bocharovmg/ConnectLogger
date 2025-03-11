using ConnectLogger.Auth.Api.Application.Entities;
using ConnectLogger.Auth.Api.Application.Interfaces.Repositories;
using ConnectLogger.Auth.Api.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace ConnectLogger.Auth.Api.Infrastructure.Persistence.Repositories;

public class UserRepository(UserDbContext dbContext) : IUserRepository
{
    private readonly UserDbContext _dbContext = dbContext;
    private bool _disposed;
    private ITransactionManager? _transactionManager = null;

    public void Dispose()
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
            if (_transactionManager != null)
            {
                _transactionManager.Dispose();
                _transactionManager = null;
            }
        }

        _disposed = true;
    }

    ~UserRepository()
    {
        Dispose(false);
    }

    public ITransactionManager CreateTransactionManager()
    {
        _transactionManager = new TransactionManager(_dbContext);
        return _transactionManager;
    }

    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var query = _dbContext
            .Users
            .Where(user =>
                user.UserName == userName);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
