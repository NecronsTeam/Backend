using CrmBackend.Database.Enums;

namespace CrmBackend.Database.Models;

public class User : BaseModel
{
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
    public required List<RolesEnum> Roles { get; set; }
}
