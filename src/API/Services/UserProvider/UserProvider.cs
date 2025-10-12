using DAL.Data;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.UserProvider;

public class UserProvider(IHttpContextAccessor context, AppDbContext appDbContext) : IUserProvider
{
    private readonly IHttpContextAccessor _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Guid> GetUserId()
    {
        var userIdStr = _context.HttpContext!.User.FindFirst("id")?.Value;

        if (userIdStr == null)
        {
            throw new InvalidOperationException("User ID claim not found.");
        }

        var userIdGuid = Guid.Parse(userIdStr);

        if (await appDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userIdGuid, CancellationToken.None) == null)
        {
            throw new InvalidOperationException("User does not exist.");
        }

        return userIdGuid;
    }

    public string GetUserRole()
    {
        var userRole = _context.HttpContext!.User.Claims
            .FirstOrDefault(c => c.Type.Contains("role", StringComparison.OrdinalIgnoreCase))?.Value;
        
        if (userRole == null)
        {
            throw new InvalidOperationException("User role claim not found.");
        }
        
        return userRole;
    } 
}