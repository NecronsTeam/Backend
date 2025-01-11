using CrmBackend.Api.Dtos;
using CrmBackend.Database;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T>
    where T : BaseModel
{
    protected readonly DatabaseContext _database;
    protected DbSet<T> table;

    public BaseRepository(DatabaseContext database)
    {
        _database = database;
        table = _database.Set<T>();
    }

    public virtual async Task<int> CreateEntityAsync(T entity)
    {
        await table.AddAsync(entity);
        await _database.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task DeleteEntityByIdAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);

        if (entity is not null)
        {
            table.Remove(entity);
            await _database.SaveChangesAsync();
        }
    }

    public virtual async Task<List<T>> GetAllEntitiesAsync()
    {
        return await table.ToListAsync();
    }

    public virtual async Task<T?> GetEntityByIdAsync(int id)
    {
        return await table.FindAsync(id);
    }

    public virtual async Task UpdateEntityAsync(int id, BasePatchDto<T> updateDto)
    {
        var entity = await GetEntityByIdAsync(id);

        if (entity is not null)
        {
            updateDto.ApplyPatch(entity);
            await _database.SaveChangesAsync();
        }
    }
}