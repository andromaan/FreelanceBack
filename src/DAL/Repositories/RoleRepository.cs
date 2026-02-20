using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Roles;
using DAL.Data;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class RoleRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<Role, int>(context, userProvider), IRoleRepository, IRoleQueries
{
    private readonly AppDbContext _context = context;

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Role>().FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }
}