using Domain.Models.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BLL.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public async Task SendNotification(Notification notification, CancellationToken cancellationToken = default)
    {
        if (notification.UserId == null)
        {
            await Clients.All.SendAsync("ReceiveNotification", notification, cancellationToken);
        }
        else
        {
            await Clients.User(notification.UserId.ToString()!)
                .SendAsync("ReceiveNotification", notification, cancellationToken);
        }
    }
}

