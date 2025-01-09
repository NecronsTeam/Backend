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
    int PreviewPhotoId,
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
    string Link,
    double MaxScore
);
