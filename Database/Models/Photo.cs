namespace CrmBackend.Database.Models;

public class Photo : BaseModel
{
    public required Guid Guid { get; set; }
    public required string Extension { get; set; }
    public required string Path { get; set; }
}
