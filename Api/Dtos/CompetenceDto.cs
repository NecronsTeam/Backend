namespace CrmBackend.Api.Dtos;

public record GetCompetenciesDto(List<OneCompetenceDto> Competencies);

public record OneCompetenceDto(int Id, string Name);

public record CreateCompetenceDto(string Name);
