using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class ActivityRepository(DatabaseContext database) : BaseRepository<Activity>(database)
{
    public async Task<List<Activity>> GetCreatedByUserActivitiesAsync(int userId) 
        => await table.Where(act => act.CreatorUserId == userId).ToListAsync();

    public async Task AddTestToActivityAsync(int activityId, double maxScore, double passingScore)
    {
        var activityTest = new ActivityTest()
        {
            ActivityId = activityId,
            MaxScore = maxScore,
            PassingScore = passingScore
        };

        await _database.ActivityTests.AddAsync(activityTest);
        await _database.SaveChangesAsync();
    }

    public async Task<ActivityTest?> GetActivityTestAsync(int activityId)
    {
        return await _database.ActivityTests.FirstOrDefaultAsync(at => at.ActivityId == activityId);
    }
}
