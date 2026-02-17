using Domain.Models.Projects;

namespace BLL.Common.Interfaces.Repositories.Categories;

public interface ICategoryQueries : IQueries<Category, int>, IUniqueQuery<Category, int>
{
    
}