using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class ActivityRepository(DatabaseContext database, CompetenceRepository competenceRepository) : BaseRepository<Activity>(database)
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

    public async Task UpdateActivityCompetences(int activityId, List<int>? competenciesIds)
    {
        if (competenciesIds is null)
            return;

        var competencies = await competenceRepository.GetCompetenciesByTheirIds(competenciesIds);
        var activity = await GetEntityByIdAsync(activityId) 
            ?? throw new BadHttpRequestException("Нет мероприятия с таким id");

        // Заменить компетенции
        activity.Competences.Clear();
        competencies.ForEach(activity.Competences.Add);

        await _database.SaveChangesAsync();
    }

    public async Task AttachPreviewPhoto(int activityId, Photo preview)
    {
        var activity = await GetEntityByIdAsync(activityId);
        if (activity is null)
            throw new BadHttpRequestException("ЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ");

        activity.PreviewPhoto = preview;

        await _database.SaveChangesAsync();
    }
}
