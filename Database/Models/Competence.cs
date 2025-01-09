namespace CrmBackend.Database.Models;

public class Competence : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public virtual List<Activity> Activities { get; set; }
}
