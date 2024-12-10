using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Database.Models;

public class Notification : BaseModel
{
    public required string Message { get; set; }
    public required bool IsSent { get; set; }
    public bool IsRead { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    public required int UserId { get; set; }
}
