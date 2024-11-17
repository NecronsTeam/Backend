using System.ComponentModel.DataAnnotations.Schema;

namespace CrmBackend.Database.Models;

public class Activity : BaseModel
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    [ForeignKey("CreatorUserId")]
    public virtual User CreatorUser { get; set; }
    public int CreatorUserId { get; set; }

    [ForeignKey("PreviewPhotoId")]
    public virtual Photo? PreviewPhoto { get; set; }
    public int PreviewPhotoId { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = [];
}
