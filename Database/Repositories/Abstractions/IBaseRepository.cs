using CrmBackend.Api.Dtos;
using CrmBackend.Database.Models;

namespace CrmBackend.Database.Repositories.Abstractions;

public interface IBaseRepository<T>
    where T : BaseModel
{
    public Task<List<T>> GetAllEntitiesAsync();
    public Task<T> GetEntityByIdAsync(int id);
    public Task<int> CreateEntityAsync(T dto);
    public Task<int> UpdateEntityAsync(int id, BaseUpdateDto<T> updateDto);
    public Task<int> DeleteEntityByIdAsync(int id);
}