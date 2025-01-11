using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class PhotoRepository(DatabaseContext database) : BaseRepository<Photo>(database)
{
    public async Task<Photo?> GetPhotoByGuidAsync(Guid photoGuid)
    {
        return await table.FirstOrDefaultAsync(photo => photo.Guid == photoGuid);
    }
}
