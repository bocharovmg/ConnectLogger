using ConnectLogger.Auth.Api.Application.Entities;
using Microsoft.EntityFrameworkCore;


namespace ConnectLogger.Auth.Api.Infrastructure.Persistence.Contexts;

public class UserDbContext : DbContext
{
    /// <summary>
    /// Учетные данные пользователей
    /// </summary>
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
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
