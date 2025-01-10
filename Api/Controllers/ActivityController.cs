using AutoMapper;

using CrmBackend.Api.Dtos;
using CrmBackend.Api.Helpers;
using CrmBackend.Api.Services;
using CrmBackend.Database.Enums;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("activity")]
[Authorize]
public class ActivityController(IMapper mapper, FilterService filterService, CompetenceRepository competenceRepository,  ActivityRepository activityRepository, UserRepository userRepository) : Controller
{
    [HttpGet]
    public async Task<ListOfActivitiesDto> GetAllActivitiesAsync
        ([FromQuery] ActivityFilterArgumentsDto? filterArgumentsDto)
    {
        var activities = await activityRepository.GetAllEntitiesAsync();

        var user = await HttpContext.User.GetUser(userRepository);
        activities = await filterService.FilterActivities(activities, filterArgumentsDto, user.Id);

        return new ListOfActivitiesDto(activities.Select(mapper.Map<OneActivityDto>).ToList());
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
    public async Task<OneActivityDto> CreateActivityAsync([FromBody] CreateActivityDto createActivityDto)
    {
        var user = await HttpContext.User.GetUser(userRepository);
        var activityModel = mapper.Map<Activity>(createActivityDto);
        activityModel.CreatorUserId = user.Id;
        activityModel.Competences = await competenceRepository.GetCompetenciesByTheirIds(createActivityDto.CompetenciesIds);
        //activityModel.PreviewPhoto = null;

        var id = await activityRepository.CreateEntityAsync(activityModel);

        return await GetOneActivityAsync(id);
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

    [HttpGet]
    [Route("chat/{activityId}")]
    public async Task<string> GetActivityChatLinkAsync(int activityId)
    {
        var activityTest = await activityRepository.GetEntityByIdAsync(activityId);
        return activityTest?.OrgChatLink ?? "";
    }
}
