using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class ActivityRepository(DatabaseContext database) : BaseRepository<Activity>(database)
{
    public async Task<List<Activity>> GetCreatedByUserActivitiesAsync(int userId) 
        => await table.Where(act => act.CreatorUserId == userId).ToListAsync();
}
