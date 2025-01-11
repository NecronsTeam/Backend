using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class CompetenceRepository(DatabaseContext database) : BaseRepository<Competence>(database)
{
    public async Task<List<Competence>> GetCompetenciesWithFilterAsync(string? searchText) =>
        searchText is null
            ? await GetAllEntitiesAsync()
            : await table.Where(competence => competence.Name.ToLower().StartsWith(searchText.ToLower())).ToListAsync();

    public async Task<bool> IsCompetenceWithNameAlreadyExists(string name)
    {
        return await table.AnyAsync(comp => comp.Name == name);
    }

    public async Task<List<Competence>> GetCompetenciesByTheirIds(List<int> ids)
    {
        var competencies = new List<Competence>();
        foreach (var id in ids)
        {
            var competence = await GetEntityByIdAsync(id);
            if (competence is not null)
                competencies.Add(competence);
        }

        return competencies;
    }
}
