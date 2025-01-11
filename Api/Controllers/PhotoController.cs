using CrmBackend.Api.Services;
using CrmBackend.Database.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("photo")]
public class PhotoController(PhotoManager photoManager, PhotoRepository photoRepository)
{
    [HttpGet]
    [Route("{guid}")]
    public async Task<IResult> GetPhotoAsync([FromRoute] Guid guid)
    {
        var photoDbObject = await photoRepository.GetPhotoByGuidAsync(guid);
        if (photoDbObject == null)
            return Results.File(fileContents: await photoManager.GetDefaultPhotoAsync(), contentType: $"image/{Path.GetExtension(photoManager.DefaultPhotoName)}");

        var photoBytes = await photoManager.GetPhotoAsync(guid);
        var photoExtensionWithoutDot = photoDbObject.Extension[1..];
        return Results.File(fileContents: photoBytes, contentType: $"image/{photoExtensionWithoutDot}", fileDownloadName: $"avatar.{photoExtensionWithoutDot}");
    }
}
