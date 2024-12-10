using CrmBackend.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Database.Repositories;

public class NotificationRepository(DatabaseContext database) : BaseRepository<Notification>(database)
{
    public async Task<List<Notification>> GetUserNotSentNotifications(int userId)
    {
        return await table.Where(x => x.UserId == userId && x.IsSent == false).ToListAsync();
    }

    public async Task MarkNotificationAsSent(Notification notification)
    {
        var notificationFromDb = await table.FirstAsync(n => n.Id == notification.Id);
        notificationFromDb.IsSent = true;
        await _database.SaveChangesAsync();
    }
}
