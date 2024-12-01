using CrmBackend.Api.Dtos;
using CrmBackend.Api.Helpers;
using CrmBackend.Database.Repositories;
using CrmBackend.Database.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("student_interaction")]
[Authorize]
public class StudentInteractionController(
    StudentInteractionRepository studentInteractionRepository,
    ActivityRepository activityRepository,
    UserRepository userRepository) : Controller
{
    [HttpPost]
    [Authorize(Roles = "Student")]
    [Route("apply/{activityId}")]
    public async Task ApplyForActivityAsync([FromRoute] int activityId)
    {
        var user = await HttpContext.User.GetUser(userRepository);
        await studentInteractionRepository.ApplyToActivityAsync(user.Id, activityId);
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task AddStudentTestResultAsync([FromBody] AddStudentTestResultDto resultDto)
    {
        var studentActivity = await studentInteractionRepository.GetStudentActivityAsync(resultDto.StudentUserId, resultDto.ActivityId) 
            ?? throw new BadHttpRequestException("Данный студент не зарегистрирован на это мероприятие");

        var test = await activityRepository.GetActivityTestAsync(resultDto.ActivityId)
            ?? throw new BadHttpRequestException("У мероприятия нет теста");

        await studentInteractionRepository.AddStudentTestResultAsync(test.Id, studentActivity.Id, resultDto.Score);

        await studentInteractionRepository.UpdateStudentActivityStatus(resultDto.ActivityId,
            resultDto.IsStudentPassedTest ? ActivityStatus.PassedTest : ActivityStatus.Rejected);
    }
}
