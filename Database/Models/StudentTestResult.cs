using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Database.Models;

public class StudentTestResult : BaseModel
{
    [ForeignKey("StudentActivityId")]
    public virtual StudentActivity StudentActivity { get; set; }
    public int StudentActivityId { get; set; }

    [ForeignKey("ActivityTestId")]
    public virtual ActivityTest ActivityTest { get; set; }
    public int ActivityTestId { get; set; }

    public double Score { get; set; }
}
