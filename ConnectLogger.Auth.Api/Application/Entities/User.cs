using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ConnectLogger.Auth.Api.Application.Entities;

[Table("User", Schema = "public")]
public class User
{
    [Key]
    public long UserId { get; set; }

    public string UserName { get; set; } = null!;
}
