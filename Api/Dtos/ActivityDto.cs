namespace CrmBackend.Api.Dtos;

public record ListOfActivitiesDto(
    List<OneActivityDto> Activities  
);

public record OneActivityDto(
    int Id,
    string Name,
    string Description,
    DateTime? DateFrom,
    DateTime? DateTo,
    int CreatorUserId,
    int PreviewPhotoId
);

public record CreateActivityDto(
    string Name,
    string Description,
    DateTime? DateFrom,
    DateTime? DateTo
);
