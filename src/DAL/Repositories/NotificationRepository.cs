using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Notifications;
using DAL.Data;
using Domain.Models.Notifications;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class NotificationRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<Notification, Guid>(context, userProvider), INotificationRepository, INotificationQueries
{
    private readonly AppDbContext _context = context;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task<Notification?> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Notifications.FirstOrDefaultAsync(n => n.UserId == userId, cancellationToken);
    }

    public override async Task<IEnumerable<Notification>> GetAllAsync(CancellationToken token)
    {
        var userId = await _userProvider.GetUserId(token);
        return await _context.Notifications.AsNoTracking()
            .Where(n => n.UserId == userId || n.UserId == null && !n.IsRead)
            .OrderByDescending(n => n.SentAt).ToListAsync(token);
    }

    public override async Task<(List<Notification> Entities, int TotalCount)> GetPaginatedAsync(int page, int pageSize,
        CancellationToken token = default)
    {
        var userId = await _userProvider.GetUserId(token);

        var query = _context.Notifications
            .Where(n => n.UserId == userId || n.UserId == null)
            .OrderByDescending(n => n.SentAt)
            .AsQueryable();

        var totalCount = await query.CountAsync(token);

        var entities = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return (entities, totalCount);
    }
    
    public async Task<Notification?> MarkAsReadAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId, cancellationToken);

        if (notification is null)
            return null;

        notification.IsRead = !notification.IsRead;
        await _context.SaveChangesAsync(cancellationToken);
        return notification;
    }

    public async Task<int> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ExecuteUpdateAsync(s => s.SetProperty(n => n.IsRead, true), cancellationToken);
    }
}