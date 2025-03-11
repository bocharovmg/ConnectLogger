namespace ConnectLogger.Connections.Logger.Consumer.Application.Entities;

public class UserConnectionLogDto
{
    public long ConnectedUserId { get; set; }

    public string UserAddress { get; set; } = null!;
}
