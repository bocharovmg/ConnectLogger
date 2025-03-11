namespace ConnectLogger.Auth.Api.Application.Interfaces.Repositories;

public interface IRepository : IDisposable
{
    ITransactionManager CreateTransactionManager();
}
