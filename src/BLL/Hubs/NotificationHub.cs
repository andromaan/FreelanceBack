using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BLL.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    // Push-only hub: сервер надсилає "ReceiveNotification" клієнту через IHubContext<NotificationHub>.
    // Позначення сповіщень як прочитаних виконується через REST API:
    //   PATCH /notification/{id}/toggle-read  — перемикає IsRead для одного сповіщення
    //   PATCH /notification/read-all          — позначає всі як прочитані
}