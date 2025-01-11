using CrmBackend.Api.Dtos;
using CrmBackend.Api.Services;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("manager")]
[Authorize(Roles = "Manager")]
public class ManagerController(ManagerRepository managerRepository, FilterService filterService) : ControllerBase
{
    [HttpGet]
    [Route("applied_students/{activityId}")]
    public async Task<StudentsAppliedOnActivityDto> GetStudentsAppliedOnActivity([FromRoute] int activityId, [FromQuery] List<int>? status, [FromQuery] string? name)
    {
        var applies = await managerRepository.GetAllAppliesOnActivityAsync(activityId);
        var appliedStudentsList = new List<AppliedStudentDto>();

        foreach (var apply in applies)
        {
            var studentAccount = apply.User.Account!;
            var appliedStudent = new AppliedStudentDto(studentAccount.Id, GetStudentFullName(studentAccount), apply.Status, "avatar_link (wiil be later)");
            appliedStudentsList.Add(appliedStudent);
        }

        appliedStudentsList = filterService.FilterAppliedStudents(appliedStudentsList, status, name);

        return new StudentsAppliedOnActivityDto(appliedStudentsList);
    }

    private static string GetStudentFullName(Account account) => $"{account.LastName} {account.FirstName} {account.MiddleName}";
}
