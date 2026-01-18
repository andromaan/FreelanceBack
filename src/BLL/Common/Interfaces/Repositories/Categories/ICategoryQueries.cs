using Domain.Models.Projects;

namespace BLL.Common.Interfaces.Repositories.Categories;

public interface ICategoryQueries : IQueries<Category, Guid>, IUniqueQuery<Category, Guid>
{
    
}