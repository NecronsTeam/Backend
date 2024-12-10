using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

namespace CrmBackend.Api.Services;

public class NotificationService(NotificationRepository notificationRepository, NotificationHub notificationHub)
{
    public async Task SendNotification(string message, int userId)
    {
        await SendNotification(new Notification()
        {
            Message = message,
            UserId = userId,
            IsRead = false,
            IsSent = false
        });
    }

    public async Task SendNotification(Notification notification)
    {
        await notificationRepository.CreateEntityAsync(notification);

        if (notificationHub.CanSendMessageToUser(notification.UserId))
            await notificationHub.Send(notification);
    }
}
