namespace CrmBackend.Database.Models;

public class Password : BaseModel
{
    public required string HashedPassword { get; set; }
    public required string CryptedPassword { get; set; }
}
