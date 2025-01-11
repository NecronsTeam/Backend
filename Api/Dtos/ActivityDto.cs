using CrmBackend.Api.Helpers;
using CrmBackend.Database.Enums;
using CrmBackend.Database.Models;

using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Api.Dtos;

public record ListOfActivitiesDto(
    List<OneActivityDto> Activities  
);

public record OneActivityDto(
    int Id,
    string Name,
    string Description,
    string OrgChatLink,
    DateTime? DateFrom,
    DateTime? DateTo,
    int CreatorUserId,
    string PreviewPhotoLink,
    List<OneCompetenceDto> Competences
);

public record CreateActivityDto(
    string Name,
    string Description,
    string OrgChatLink,
    DateTime? DateFrom,
    DateTime? DateTo,
    List<int> CompetenciesIds
);

public record TestDto(
    int ActivityId,
    double MaxScore,
    double PassingScore
);

public record ActivityFilterArgumentsDto(
    string? Name,
    List<int>? Competencies,
    bool? Applied,
    List<int>? Status,
    bool? Owned
);

public record PatchActivityDto(
    string? Name,
    string? Description,
    string? OrgChatLink,
    DateTime? DateFrom,
    DateTime? DateTo,
    List<int>? CompetenciesIds
) : BasePatchDto<Activity>;

public record AttachActivityPreviewDto(
    [OnlyPhoto]
    IFormFile Preview
);