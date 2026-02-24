using Domain.Models.Notifications;

namespace BLL.Common.Interfaces.Repositories.Notifications;

public interface INotificationRepository : IRepository<Notification, Guid>
{
    Task<Notification?> MarkAsReadAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken);
    Task<int> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken);
}

