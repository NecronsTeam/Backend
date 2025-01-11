using CrmBackend.Api.Dtos;
using CrmBackend.Database.Models;

namespace CrmBackend.Database.Repositories.Abstractions;

public interface IBaseRepository<T>
    where T : BaseModel
{
    public Task<List<T>> GetAllEntitiesAsync();
    public Task<T?> GetEntityByIdAsync(int id);
    public Task<int> CreateEntityAsync(T dto);
    public Task UpdateEntityAsync(int id, BasePatchDto<T> updateDto);
    public Task DeleteEntityByIdAsync(int id);
}