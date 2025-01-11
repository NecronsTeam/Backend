using System;

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
public class ActivityController(IMapper mapper,
                                FilterService filterService,
                                CompetenceRepository competenceRepository,
                                ActivityRepository activityRepository,
                                UserRepository userRepository,
                                PhotoManager photoManager,
                                PhotoRepository photoRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ListOfActivitiesDto> GetAllActivitiesAsync
        ([FromQuery] ActivityFilterArgumentsDto? filterArgumentsDto)
    {
        var activities = await activityRepository.GetAllEntitiesAsync();

        var user = await HttpContext.User.GetUser(userRepository);
        activities = await filterService.FilterActivities(activities, filterArgumentsDto, user.Id);

        return new ListOfActivitiesDto(activities.Select(MapActivityToDto).ToList());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<OneActivityDto> GetOneActivityAsync([FromRoute] int id)
    {
        var activityFromDb = await activityRepository.GetEntityByIdAsync(id);
        if (activityFromDb is null)
            throw new BadHttpRequestException("Мероприятия с таким id не существует");

        return MapActivityToDto(activityFromDb);
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<OneActivityDto> CreateActivityAsync([FromBody] CreateActivityDto createActivityDto)
    {
        var user = await HttpContext.User.GetUser(userRepository);
        var activityModel = mapper.Map<Activity>(createActivityDto);
        activityModel.CreatorUserId = user.Id;
        activityModel.Competences = await competenceRepository.GetCompetenciesByTheirIds(createActivityDto.CompetenciesIds);

        var id = await activityRepository.CreateEntityAsync(activityModel);

        return await GetOneActivityAsync(id);
    }

    [HttpPatch]
    [Authorize(Roles = "Manager")]
    [Route("{id}")]
    public async Task<OneActivityDto> PatchActivityAsync([FromRoute] int id, [FromBody] PatchActivityDto patchActivityDto)
    {
        if (await activityRepository.GetEntityByIdAsync(id) is null)
            throw new BadHttpRequestException("Мероприятия с таким id не существует");

        await activityRepository.UpdateEntityAsync(id, patchActivityDto);
        await activityRepository.UpdateActivityCompetences(id, patchActivityDto.CompetenciesIds);

        return await GetOneActivityAsync(id);
    }

    [HttpPost]
    [Route("test")]
    public async Task AddTestToAcitivityAsync([FromBody] TestDto addTestDto)
    {
        _ = await activityRepository.GetEntityByIdAsync(addTestDto.ActivityId) 
            ?? throw new BadHttpRequestException("Мероприятия с таким id не существует");

        if (addTestDto.MaxScore <= 0)
            throw new BadHttpRequestException("Максимальный балл теста не может быть меньше или равен нулю");

        await activityRepository.AddTestToActivityAsync(addTestDto.ActivityId, addTestDto.MaxScore, addTestDto.PassingScore);
    }

    [HttpGet]
    [Route("test/{activityId}")]
    public async Task<TestDto?> GetActivityTestAsync([FromRoute] int activityId)
    {
        if (await activityRepository.GetEntityByIdAsync(activityId) is null)
            throw new BadHttpRequestException("Мероприятия с таким id не существует");

        var activityTest = await activityRepository.GetActivityTestAsync(activityId);
        return mapper.Map<TestDto?>(activityTest);
    }

    [HttpGet]
    [Route("chat/{activityId}")]
    public async Task<string> GetActivityChatLinkAsync([FromRoute] int activityId)
    {
        if (await activityRepository.GetEntityByIdAsync(activityId) is null)
            throw new BadHttpRequestException("Мероприятия с таким id не существует");

        var activityTest = await activityRepository.GetEntityByIdAsync(activityId);
        return activityTest?.OrgChatLink ?? "";
    }

    [HttpPost]
    [Route("preview/{activityId}")]
    public async Task<string> AttachPreviewPhoto([FromRoute] int activityId, [FromForm] AttachActivityPreviewDto previewDto)
    {
        var activity = await activityRepository.GetEntityByIdAsync(activityId);
        if (activity is null)
            throw new BadHttpRequestException("Мероприятия с таким id не существует");

        var guid = await photoManager.UploadPhotoAsync(previewDto.Preview);
        var photoObjectFromDb = await photoRepository.GetPhotoByGuidAsync(guid);
        await activityRepository.AttachPreviewPhoto(activityId, photoObjectFromDb!);

        return photoManager.GetLinkToPhotoByGuid(guid);
    }

    private OneActivityDto MapActivityToDto(Activity activity)
    {
        return new OneActivityDto(
            activity.Id,
            activity.Name,
            activity.Description,
            activity.OrgChatLink,
            activity.DateFrom,
            activity.DateTo,
            activity.CreatorUserId,
            activity.PreviewPhoto is not null ? photoManager.GetLinkToPhotoByGuid(activity.PreviewPhoto.Guid) : "",
            activity.Competences.Select(mapper.Map<OneCompetenceDto>).ToList()
            );
    }
}
