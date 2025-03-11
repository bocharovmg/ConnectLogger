using ConnectLogger.Auth.Api.Application.Dtos;
using ConnectLogger.Auth.Api.Application.Enums;
using ConnectLogger.Auth.Api.Application.Interfaces.Repositories;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignUp;


namespace ConnectLogger.Auth.Api.Application.UseCases.User;

public class SignUpUseCase(IUserRepository userRepository) : ISignUpUseCase
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<SignUpResponse> ExecuteAsync(SignUpRequest request, CancellationToken cancellationToken = default)
    {
        using var transactionManager = _userRepository.CreateTransactionManager();
        
        await transactionManager.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await _userRepository.GetUserByUserNameAsync(request.UserName, cancellationToken);
            if (user != null)
            {
                return BaseResponse.Error<SignUpResponse>("Пользователь уже зарегистрирован.", ResponseErrorTypeEnum.InvalidProperties);
            }

            user = new Entities.User
            {
                UserName = request.UserName,
            };
            await _userRepository.AddUserAsync(user, cancellationToken);
            await transactionManager.CommitTransactionAsync(cancellationToken);

            return new SignUpResponse
            {
                IsSuccess = true,
                User = new UserDto
                {
                    UserId = user.UserId,
                }
            };
        }
        catch
        {
            await transactionManager.RollbackTransactionAsync(cancellationToken);

            throw;
        }
    }
}
