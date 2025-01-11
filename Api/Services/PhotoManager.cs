using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

namespace CrmBackend.Api.Services;

public class PhotoManager(PhotoRepository photoRepository, IConfiguration configuration, ServerPathService serverPathService)
{
    public readonly string PhotoDirectory = configuration["Storage:PhotoPath"] ?? throw new KeyNotFoundException("Set Storage:PhotoPath in config");
    public readonly string DefaultPhotoName = configuration["Storage:DefaultPhotoName"] ?? throw new KeyNotFoundException("Set Storage:DefaultPhotoName in config");

    /// <summary>
    ///     Загружает фото пользователя на сервер, сохраняет информацию об этом в БД, и отдаёт его GUID
    /// </summary>
    /// <param name="photo">IFormFile фотка, которую нужно загрузить</param>
    /// <returns>GUID фото</returns>
    public async Task<Guid> UploadPhotoAsync(IFormFile photo)
    {
        var guid = Guid.NewGuid();
        var pathToPhoto = GetPathToPhotoByGuid(guid);

        using var filestream = new FileStream(path: pathToPhoto, mode: FileMode.Create, FileAccess.Write);
        await photo.CopyToAsync(filestream);

        var photoObjectForDb = new Photo()
        {
            Guid = guid,
            Path = pathToPhoto,
            Extension = Path.GetExtension(photo.FileName)
        };

        await photoRepository.CreateEntityAsync(photoObjectForDb);

        return guid;
    }

    /// <summary>
    ///     Получает фото в байтовом виде по его GUID
    /// </summary>
    /// <param name="photoGuid">GUID фото</param>
    /// <returns>Массив байт (в котором фото)</returns>
    public async Task<byte[]> GetPhotoAsync(Guid photoGuid)
    {
        var photoPath = GetPathToPhotoByGuid(photoGuid);
        return await GetPhotoBytesByPathAsync(photoPath);
    }

    /// <summary>
    ///     Получает дефолтное фото в байтовом виде
    /// </summary>
    /// <returns>Массив байт (в котором фото)</returns>
    public async Task<byte[]> GetDefaultPhotoAsync()
    {
        return await GetPhotoBytesByPathAsync(DefaultPhotoName);
    }


    private static async Task<byte[]> GetPhotoBytesByPathAsync(string photoPath)
    {
        using var photoFileStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read);

        var photoBytes = new byte[photoFileStream.Length];
        _ = await photoFileStream.ReadAsync(photoBytes);

        return photoBytes;
    }

    public async Task<string> GetLinkToPhotoByPhotoId(int photoId) => GetLinkToPhotoByGuid((await photoRepository.GetEntityByIdAsync(photoId)).Guid);

    public string GetLinkToPhotoByGuid(Guid guid) => $"{serverPathService.GetServerPath()}/photo/{guid}";

    private string GetPathToPhotoByGuid(Guid photoGuid) => Path.Combine(PhotoDirectory, photoGuid.ToString());
}
