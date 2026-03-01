using Domain.Models.Notifications;

namespace BLL.ViewModels.Notification;

public class FilterNotificationVM
{
    public bool? IsRead { get; set; }
    public NotificationType? NotificationType { get; set; }
}