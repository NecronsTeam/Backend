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
    //[Authorize(Roles = "Manager")]
    [Route("add_student_test_result")]
    public async Task AddStudentTestResultAsync([FromBody] AddStudentTestResultDto resultDto)
    {
        var studentActivity = await studentInteractionRepository.GetStudentActivityAsync(resultDto.StudentUserId, resultDto.ActivityId) 
            ?? throw new BadHttpRequestException("Данный студент не зарегистрирован на это мероприятие");

        var test = await activityRepository.GetActivityTestAsync(resultDto.ActivityId)
            ?? throw new BadHttpRequestException("У мероприятия нет теста");

        if (resultDto.Score < 0)
            throw new BadHttpRequestException("Результат теста не может быть меньше нуля");
        if (resultDto.Score > test.MaxScore)
            throw new BadHttpRequestException("Результат теста не может быть больше максимального количества баллов за тест");


        var studentTestResultIfExists = await studentInteractionRepository.GetStudentTestResultAsync(studentActivity.Id);
        if (studentTestResultIfExists is not null)
            throw new BadHttpRequestException("У данного студента уже есть результат за этот тест");

        await studentInteractionRepository.AddStudentTestResultAsync(test.Id, studentActivity.Id, resultDto.Score);

        await studentInteractionRepository.UpdateStudentActivityStatus(studentActivity.Id,
            resultDto.IsStudentPassedTest ? ActivityStatus.PassedTest : ActivityStatus.Rejected);
    }
}
