using CrmBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Password> Passwords { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public DbSet<ActivityTest> ActivityTests { get; set; }
    public DbSet<Competence> Competences { get; set; }
    public DbSet<StudentActivity> StudentActivities { get; set; }
    public DbSet<StudentTestResult> StudentTestResults { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    private IConfiguration Config { get; init; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration config) : base(options)
    {
        Config = config;

        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var host = Config["DatabaseConnection:Host"];
        var databaseName = Config["DatabaseConnection:DatabaseName"];
        var username = Config["DatabaseConnection:Username"];
        var password = Config["DatabaseConnection:Password"];

        optionsBuilder.UseNpgsql($"Host={host};Database={databaseName};Username={username};Password={password}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка автоматической промежуточной таблицы
        modelBuilder.Entity<Activity>()
            .HasMany(a => a.Photos)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ActivityPhotos",
                j => j.HasOne<Photo>().WithMany().HasForeignKey("PhotoId"),
                j => j.HasOne<Activity>().WithMany().HasForeignKey("ActivityId")
            );
    }
}