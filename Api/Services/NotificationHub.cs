using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

using Microsoft.AspNetCore.SignalR;

namespace CrmBackend.Api.Services;

public class NotificationHub(NotificationRepository notificationRepository) : Hub
{
    private static List<int> ConnectedUsersIds { get; set; } = [];

    public async Task Send(Notification notification)
    {
        await Clients.User(notification.UserId.ToString()).SendAsync("Notify", notification.Message);

        await notificationRepository.MarkNotificationAsSent(notification);
    }

    public bool CanSendMessageToUser(int userId) => ConnectedUsersIds.Contains(userId);

    public async override Task OnConnectedAsync()
    {
        var stringedUserId = Context.UserIdentifier;
        if (!int.TryParse(stringedUserId, out var userId))
        {
            Context.Abort();
        }

        ConnectedUsersIds.Add(userId);
        await base.OnConnectedAsync();
        await SendNotSentCollectedNotifications(userId);
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        var stringedUserId = Context.UserIdentifier;
        if (int.TryParse(stringedUserId, out var userId))
        {
            ConnectedUsersIds.Remove(userId);
        }

        await base.OnDisconnectedAsync(exception);
    }

    private async Task SendNotSentCollectedNotifications(int userId)
    {
        var notSentNotifications = await notificationRepository.GetUserNotSentNotifications(userId);

        foreach (var notification in notSentNotifications)
            await Send(notification);
    }
}
