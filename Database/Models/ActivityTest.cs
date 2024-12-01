using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Database.Models;

public class ActivityTest : BaseModel
{
    public string Link { get; set; } = string.Empty;
    public double MaxScore { get; set; }

    [ForeignKey("ActivityId")]
    public virtual Activity Activity { get; set; }
    public int ActivityId { get; set; }
}
