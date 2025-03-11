using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ConnectLogger.Connections.Api.Infrastructure.Persistence.Contexts;
using ConnectLogger.Connections.Api.Application.Entities;


namespace ConnectLogger.Connections.Api.Presentation.Controllers;

[ApiController]
[Route("log")]
public class LogController(ConnectLoggerDbContext connectLoggerDbContext)
    : ControllerBase
{
    private readonly ConnectLoggerDbContext _connectLoggerDbContext = connectLoggerDbContext;

    [HttpGet(Name = "GetLogs")]
    [EnableQuery(EnsureStableOrdering = false)]
    [ProducesResponseType(typeof(UserConnectionLog[]), StatusCodes.Status200OK)]
    public IEnumerable<UserConnectionLog> GetLogsAsync()
    {
        return _connectLoggerDbContext.UserConnectionLogs.AsQueryable();
    }
}
