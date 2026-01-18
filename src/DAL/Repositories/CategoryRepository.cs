using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Categories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CategoryRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Category, Guid>(appDbContext, userProvider), ICategoryRepository, ICategoryQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<bool> IsUniqueAsync(Category entity, CancellationToken token)
    {
        return !await _appDbContext.Set<Category>().AnyAsync(c => c.Name == entity.Name, token);
    }
}