using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ConnectLogger.Connections.Api.Application.Entities;

[Table("UserConnectionLog", Schema = "public")]
public class UserConnectionLog
{
    [Key]
    public long UserConnectionLogId { get; set; }

    public DateTime LogDateTime { get; set; }

    public long ConnectedUserId { get; set; }

    public string UserIp4Address { get; set; } = null!;

    public string UserIp6Address { get; set; } = null!;
}
