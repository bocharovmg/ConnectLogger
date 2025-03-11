using ConnectLogger.Auth.Api.Application.Entities;


namespace ConnectLogger.Auth.Api.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository
{
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken = default);

    Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}
