using System.Xml.Linq;

using CrmBackend.Api.Dtos;
using CrmBackend.Database.Enums;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

using Microsoft.AspNetCore.Mvc;

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

    public List<AppliedStudentDto> FilterAppliedStudents(List<AppliedStudentDto> appliedStudents, List<int>? status, string? name)
    {
        var appliedStudentsCopied = appliedStudents.Select(appStud => appStud); // Copying list to not change source list

        if (!string.IsNullOrEmpty(name))
        {
            appliedStudentsCopied = appliedStudentsCopied.Where(appStud => IsSearchNameInFullName(appStud.FullName, name));
        }

        if (status?.Count > 0)
        {
            var filteredStudents = appliedStudentsCopied.Where(appStud => status.Contains((int)appStud.Status));
            appliedStudentsCopied = appliedStudentsCopied.Intersect(filteredStudents);
        }

        return appliedStudentsCopied.ToList();
    }

    private static bool IsSearchNameInFullName(string fullName, string searchName)
    {
        var nameParts = fullName.Split(' ');

        if (fullName.StartsWith(searchName))
            return true;

        return nameParts.Any(part => part.Contains(searchName));
    }
}
