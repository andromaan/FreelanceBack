using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Categories;
using BLL.Services;
using BLL.ViewModels.Project;
using Domain.Models.Projects;

namespace BLL.Commands.Projects.Handlers;

public class UpdateProjectCategoriesHandler(
    ICategoryQueries categoryQueries
    ) : IUpdateHandler<Project, UpdateProjectCategoriesVM>
{
    public async Task<ServiceResponse?> HandleAsync(Project existingEntity, UpdateProjectCategoriesVM updateModel,
        CancellationToken cancellationToken)
    {
        var categoriesIds = updateModel.CategoryIds.Distinct();

        existingEntity.Categories.Clear();

        foreach (var categId in categoriesIds)
        {
            var existingCategory = await categoryQueries.GetByIdAsync(categId, cancellationToken);
            if (existingCategory == null)
            {
                return ServiceResponse.NotFound($"Category with id {categId} not found");
            }

            existingEntity.Categories.Add(existingCategory);
        }
        
        return ServiceResponse.Ok();
    }
}