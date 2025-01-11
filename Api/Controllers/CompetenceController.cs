using CrmBackend.Api.Dtos;
using CrmBackend.Database.Repositories;
using CrmBackend.Database.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("competence")]
[Authorize]
public class CompetenceController(IMapper mapper, CompetenceRepository competenceRepository)
{
    [HttpGet]
    public async Task<GetCompetenciesDto> GetCompetencies([FromQuery] string? searchText)
    {
        var competencies = await competenceRepository.GetCompetenciesWithFilterAsync(searchText);

        return new GetCompetenciesDto(mapper.Map<List<OneCompetenceDto>>(competencies));
    }

    [HttpPost]
    public async Task<OneCompetenceDto> CreateCompetence([FromBody] CreateCompetenceDto competenceDto)
    {
        if (await competenceRepository.IsCompetenceWithNameAlreadyExists(competenceDto.Name))
            throw new BadHttpRequestException("Компетенция с таким именем уже существует");

        var id = await competenceRepository.CreateEntityAsync(new Competence() { Name = competenceDto.Name });
        return new OneCompetenceDto(id, competenceDto.Name);
    }
}
