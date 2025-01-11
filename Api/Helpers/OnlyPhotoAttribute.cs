using System.ComponentModel.DataAnnotations;

namespace CrmBackend.Api.Helpers;

[AttributeUsage(AttributeTargets.Parameter, Inherited = true)]
public class OnlyPhotoAttribute : ValidationAttribute
{
    private static readonly string[] ValidFilesExtenstions = [".png", ".webp", ".gif", ".jpg", ".jpeg"];

    public override bool IsValid(object? value)
    {
        if (value is not IFormFile file)
            throw new BadHttpRequestException("Неправильный формат данных. Должен быть отправлен файл.");

        var fileExtenstion = Path.GetExtension(file.Name);
        if (!ValidFilesExtenstions.Contains(fileExtenstion))
            throw new BadHttpRequestException("Данный файл не является изображением.");

        return true;
    }
}
