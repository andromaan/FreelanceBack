using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Skills;
using DAL.Data;
using Domain.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SkillRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Skill, int>(appDbContext, userProvider), ISkillRepository, ISkillQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<bool> IsUniqueAsync(Skill entity, CancellationToken token)
    {
        return !await _appDbContext.Set<Skill>().AnyAsync(c => c.Name == entity.Name, token);
    }
}

