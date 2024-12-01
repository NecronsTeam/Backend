using AutoMapper;

using CrmBackend.Api.Dtos;
using CrmBackend.Api.Helpers;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("activity")]
[Authorize]
public class ActivityController(IMapper mapper, ActivityRepository activityRepository, UserRepository userRepository) : Controller
{
    [HttpGet]
    public async Task<ListOfActivitiesDto> GetAllActivitiesAsync()
    {
        var activitiesFromDb = await activityRepository.GetAllEntitiesAsync();

        return new ListOfActivitiesDto(activitiesFromDb.Select(mapper.Map<OneActivityDto>).ToList());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<OneActivityDto> GetOneActivityAsync([FromRoute] int id)
    {
        var activityFromDb = await activityRepository.GetEntityByIdAsync(id);

        return mapper.Map<OneActivityDto>(activityFromDb);
    }

    [HttpPost]
    //[Authorize(Roles = "Manager")]
    public async Task CreateActivityAsync([FromBody] CreateActivityDto createActivityDto)
    {
        var user = await HttpContext.User.GetUser(userRepository);
        var activityModel = mapper.Map<Activity>(createActivityDto);
        activityModel.CreatorUserId = user.Id;
        //activityModel.PreviewPhoto = null;

        await activityRepository.CreateEntityAsync(activityModel);
    }

    [HttpGet]
    [Route("owned")]
    //[Authorize(Roles = "Manager")]
    public async Task<ListOfActivitiesDto> GetCreatedByUserActivities()
    {
        var user = await HttpContext.User.GetUser(userRepository);
        var activitiesFromDb = await activityRepository.GetCreatedByUserActivitiesAsync(user.Id);

        return new ListOfActivitiesDto(activitiesFromDb.Select(mapper.Map<OneActivityDto>).ToList());
    }

    [HttpPost]
    [Route("test")]
    public async Task AddTestToAcitivityAsync(TestDto addTestDto)
    {
        _ = await activityRepository.GetEntityByIdAsync(addTestDto.ActivityId) 
            ?? throw new BadHttpRequestException("Мероприятия с таким идентификатором не существует");

        if (addTestDto.MaxScore <= 0)
            throw new BadHttpRequestException("Максимальный балл теста не может быть меньше или равен нулю");

        await activityRepository.AddTestToActivityAsync(addTestDto.ActivityId, addTestDto.Link, addTestDto.MaxScore);
    }

    [HttpGet]
    [Route("test/{activityId}")]
    public async Task<TestDto?> GetActivityTestAsync(int activityId)
    {
        var activityTest = await activityRepository.GetActivityTestAsync(activityId);
        return mapper.Map<TestDto?>(activityTest);
    }
}
