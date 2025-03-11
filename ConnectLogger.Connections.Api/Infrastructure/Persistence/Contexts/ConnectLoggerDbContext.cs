using ConnectLogger.Connections.Api.Application.Entities;
using Microsoft.EntityFrameworkCore;


namespace ConnectLogger.Connections.Api.Infrastructure.Persistence.Contexts;

public class ConnectLoggerDbContext : DbContext
{
    /// <summary>
    /// Лог подключений пользователей
    /// </summary>
    public DbSet<UserConnectionLog> UserConnectionLogs { get; set; }

    public ConnectLoggerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
    }
}
