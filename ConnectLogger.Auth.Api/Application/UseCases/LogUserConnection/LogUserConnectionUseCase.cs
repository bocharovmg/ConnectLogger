using ConnectLogger.Auth.Api.Application.Dtos;
using ConnectLogger.Auth.Api.Application.Interfaces.Services;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.LogUserConnection;
using ConnectLogger.Auth.Api.Application.Options;
using Microsoft.Extensions.Options;


namespace ConnectLogger.Auth.Api.Application.UseCases.LogUserConnection;

public class LogUserConnectionUseCase(
    IOptions<KafkaTopicOptions> kafkaTopicOptions,
    IKafkaProducerService kafkaProducerService)
    : ILogUserConnectionUseCase
{
    private readonly KafkaTopicOptions _kafkaTopicOptions = kafkaTopicOptions.Value;
    private readonly IKafkaProducerService _kafkaProducerService = kafkaProducerService;

    public async Task<LogUserConnectionResponse> ExecuteAsync(LogUserConnectionRequest request, CancellationToken cancellationToken = default)
    {
        var userConnectionLog = new UserConnectionLogDto
        {
            ConnectedUserId = request.UserId,
            UserAddress = request.UserAddress,
        };
        await _kafkaProducerService.ProduceAsync(_kafkaTopicOptions.Topic, userConnectionLog, cancellationToken);

        return new LogUserConnectionResponse
        {
            IsSuccess = true
        };
    }
}
