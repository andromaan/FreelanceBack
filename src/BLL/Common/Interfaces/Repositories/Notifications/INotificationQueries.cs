using Domain.Models.Notifications;

namespace BLL.Common.Interfaces.Repositories.Notifications;

public interface INotificationQueries : IQueries<Notification, Guid>, IByUserQuery<Notification, Guid>
{
}