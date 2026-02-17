using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Categories;
using BLL.Services;
using BLL.ViewModels.Project;
using Domain.Models.Projects;

namespace BLL.Commands.Projects.Handlers;

public class GetAllFilteredProjectsHandler(ICategoryQueries categoryQueries)
    : IGetAllFilteredHandler<Project, FilterProjectVM>
{
    public async Task<(ServiceResponse response, int? filteredTotalCount, List<Project>? filteredEntities)> HandleAsync(
        List<Project> entities, FilterProjectVM filter,
        CancellationToken cancellationToken)
    {
        List<Project> filteredEntities = entities;

        if (filter.Title != null)
        {
            filteredEntities = entities.Where(e => e.Title.Contains(filter.Title)).ToList();
        }

        if (filter.Description != null)
        {
            filteredEntities = entities.Where(e => e.Description != null && e.Description.Contains(filter.Description))
                .ToList();
        }

        if (filter.BudgetMin != null)
        {
            filteredEntities = entities.Where(e => e.Budget >= filter.BudgetMin).ToList();
        }

        if (filter.BudgetMax != null)
        {
            filteredEntities = entities.Where(e => e.Budget <= filter.BudgetMax).ToList();
        }

        if (filter.Status != null)
        {
            filteredEntities = entities.Where(e => e.Status == filter.Status).ToList();
        }

        if (filter.DeadlineMax != null)
        {
            filteredEntities = entities.Where(e => e.Deadline <= filter.DeadlineMax).ToList();
        }

        if (filter.CategoryIds != null && filter.CategoryIds.Count > 0)
        {
            foreach (var filterCategoryId in filter.CategoryIds)
            {
                var category = await categoryQueries.GetByIdAsync(filterCategoryId, cancellationToken);
                if (category == null)
                {
                    return (ServiceResponse.NotFound($"Category with id {filterCategoryId} not found"), null, null);
                }

                filteredEntities = entities.Where(e => e.Categories.Any(c => c.Id == filterCategoryId)).ToList();
            }
        }
        
        var totalCount = filteredEntities.Count;

        return (ServiceResponse.Ok(), totalCount, filteredEntities);
    }
}