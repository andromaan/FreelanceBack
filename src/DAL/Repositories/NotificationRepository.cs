using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Notifications;
using DAL.Data;
using Domain.Models.Notifications;

namespace DAL.Repositories;

public class NotificationRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<Notification, Guid>(context, userProvider), INotificationRepository, INotificationQueries
{
}