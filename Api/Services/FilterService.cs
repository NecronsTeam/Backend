using CrmBackend.Api.Dtos;
using CrmBackend.Database.Enums;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

namespace CrmBackend.Api.Services;

public class FilterService(StudentInteractionRepository studentInteractionRepository)
{
    public async Task<List<Activity>> FilterActivities(List<Activity> activities, ActivityFilterArgumentsDto? filterArgs, int userId)
    {
        var activitiesList = activities.Select(act => act); // Copying list to not change source list

        if (filterArgs is null)
            return activities;

        if (!string.IsNullOrEmpty(filterArgs.Name))
        {
            activitiesList = activitiesList.Where(act => act.Name.StartsWith(filterArgs.Name));
        }

        if (filterArgs.Competencies?.Count > 0)
        {
            activitiesList = activitiesList.Where(act => act.Competences.Select(c => c.Id).Intersect(filterArgs.Competencies).Any());
        }

        if (filterArgs.Applied is true)
        {
            var studentActivities = await studentInteractionRepository.GetAllStudentActivitiesAsync(userId);
            activitiesList = activitiesList.Intersect(studentActivities.Select(sa => sa.Activity));
        }

        if (filterArgs.Status?.Count > 0)
        {
            var studentActivities = await studentInteractionRepository.GetAllStudentActivitiesAsync(userId);
            var filteredStudentActivities = studentActivities.Where(sa => filterArgs.Status.Contains((int)sa.Status));
            activitiesList = activitiesList.Intersect(filteredStudentActivities.Select(sa => sa.Activity));
        }

        if (filterArgs.Owned is true)
        {
            activitiesList = activitiesList.Where(act => act.CreatorUserId == userId);
        }

        return activitiesList.ToList();
    }
}
