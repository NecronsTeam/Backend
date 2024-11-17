using CrmBackend.Database.Models;

namespace CrmBackend.Api.Dtos;

public abstract class BaseUpdateDto<TModel>
    where TModel : BaseModel
{
    public abstract TModel UpdateEntity(TModel entityToUpdate);
}