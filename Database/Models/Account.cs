namespace CrmBackend.Database.Models;

public class Account : BaseModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? TelegramLink { get; set; }

    public virtual User User { get; set; }
}
