using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database;

public class DatabaseContext : DbContext
{
    private IConfiguration _config { get; init; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration config) : base(options)
    {
        _config = config;

        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var host = _config["DatabaseConnection:Host"];
        var databaseName = _config["DatabaseConnection:DatabaseName"];
        var username = _config["DatabaseConnection:Username"];
        var password = _config["DatabaseConnection:Password"];

        optionsBuilder.UseNpgsql($"Host={host};Database={databaseName};Username={username};Password={password}");
    }
}