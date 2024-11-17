namespace CrmBackend.Database.Models;

public class Photo
{
    public int Id { get; set; }
    public required Guid Guid { get; set; }
    public required string Path { get; set; }
}
