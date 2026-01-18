using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Categories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Projects;

namespace DAL.Repositories;

public class CategoryRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Category, Guid>(appDbContext, userProvider), ICategoryRepository, ICategoryQueries
{
}