using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Database.Models;

public class ActivityTest : BaseModel
{
    public double MaxScore { get; set; }
    public double PassingScore { get; set; }

    [ForeignKey("ActivityId")]
    public virtual Activity Activity { get; set; }
    public int ActivityId { get; set; }
}
