using System.ComponentModel.DataAnnotations.Schema;

using CrmBackend.Database.Enums;

namespace CrmBackend.Database.Models;

public class StudentActivity : BaseModel
{
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    public int UserId { get; set; }

    [ForeignKey("ActivityId")]
    public virtual User Activity { get; set; }
    public int ActivityId { get; set; }

    public ActivityStatus Status { get; set; }

    [ForeignKey("StudentTestResultId")]
    public virtual StudentTestResult? StudentTestResult { get; set; }
    public int? StudentTestResultId { get; set; }
}
