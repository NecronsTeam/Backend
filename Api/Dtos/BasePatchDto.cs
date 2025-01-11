using CrmBackend.Database.Models;

namespace CrmBackend.Api.Dtos;

public abstract record BasePatchDto<TModel>
    where TModel : BaseModel
{
    public virtual void ApplyPatch(TModel objectFromDb)
    {
        var typeOfDto = GetType();
        var dtoProperties = typeOfDto.GetProperties();

        var modelProperties = typeof(TModel).GetProperties();

        foreach (var property in dtoProperties)
        {
            var value = property.GetValue(this);

            if (value is not null)
            {
                var objectFromDbProperty = modelProperties.FirstOrDefault(prop => prop.Name.ToLower() == property.Name.ToLower());

                if (objectFromDbProperty is not null)
                    objectFromDbProperty.SetValue(objectFromDb, value);
            }
        }
    }
}