using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class CompetenceRepository(DatabaseContext database) : BaseRepository<Competence>(database)
{
    public async Task<List<Competence>> GetCompetenciesWithFilterAsync(string? searchText) =>
        searchText is null
            ? await GetAllEntitiesAsync()
            : await table.Where(competence => competence.Name.StartsWith(searchText)).ToListAsync();

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
