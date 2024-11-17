using System.ComponentModel.DataAnnotations.Schema;

using CrmBackend.Database.Enums;

namespace CrmBackend.Database.Models;

public class User : BaseModel
{
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
    public required List<RolesEnum> Roles { get; set; }

    [ForeignKey("AccountId")]
    public virtual Account? Account { get; set; }
}
