using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class ManagerRepository(DatabaseContext database)
{
    public async Task<List<StudentActivity>> GetAllAppliesOnActivityAsync(int activityId)
    {
        return await database.StudentActivities.Where(sa => sa.ActivityId == activityId).ToListAsync();
    }
}
