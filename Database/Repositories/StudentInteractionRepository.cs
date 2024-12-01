using CrmBackend.Database.Enums;
using CrmBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class StudentInteractionRepository(DatabaseContext database, ActivityRepository activityRepository)
{
    public async Task ApplyToActivityAsync(int userId, int activityId)
    {
        var studentActivity = new StudentActivity()
        {
            UserId = userId,
            ActivityId = activityId,
            Status = Enums.ActivityStatus.Registered,
        };

        database.StudentActivities.Add(studentActivity);

        await database.SaveChangesAsync();
    }

    public async Task<StudentActivity?> GetStudentActivityAsync(int userId, int activityid)
    {
        return await database.StudentActivities.FirstOrDefaultAsync(sa => sa.UserId == userId && sa.ActivityId == activityid);
    }

    public async Task AddStudentTestResultAsync(int testId, int studentActivityId, double score)
    {
        var studentTestResult = new StudentTestResult()
        {
            ActivityTestId = testId,
            StudentActivityId = studentActivityId,
            Score = score,
        };

        await database.StudentTestResults.AddAsync(studentTestResult);
        await database.SaveChangesAsync();
    }

    public async Task UpdateStudentActivityStatus(int studentActivityId, ActivityStatus activityStatus)
    {
        var studentActivity = await database.StudentActivities.FirstOrDefaultAsync(sa => sa.Id == studentActivityId)
            ?? throw new ArgumentNullException(nameof(studentActivityId));

        studentActivity.Status = activityStatus;
        await database.SaveChangesAsync();
    }
}
