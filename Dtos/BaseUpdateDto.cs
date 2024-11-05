using CrmBackend.Models;

namespace CrmBackend.Dtos;

public abstract class BaseUpdateDto<TModel>
    where TModel : BaseModel
{
    public abstract TModel UpdateEntity(TModel entityToUpdate);
}