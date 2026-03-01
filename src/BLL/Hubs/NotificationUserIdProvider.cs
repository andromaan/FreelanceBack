using Microsoft.AspNetCore.SignalR;

namespace BLL.Hubs;

/// <summary>
/// Tells SignalR which claim to use as the user identifier.
/// Must match the value passed to Clients.User(userId).
/// </summary>
public class NotificationUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        // JWT має claim "id" = user.Id (Guid)
        var userId = connection.User.FindFirst("id")?.Value;
        return userId;
    }
}


