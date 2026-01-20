using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Categories;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Project;
using Domain;
using MediatR;

namespace BLL.Commands.Projects;

public record UpdateProjectCategoryCommand : IRequest<ServiceResponse>
{
    public required Guid ProjectId { get; init; }
    public required UpdateProjectCategoriesVM Vm { get; init; }
}

public class UpdateProjectCategoryCommandHandler(
    IUserProvider userProvider,
    IMapper mapper,
    IProjectRepository projectRepository,
    IProjectQueries projectQueries,
    ICategoryQueries categoryQueries
) : IRequestHandler<UpdateProjectCategoryCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateProjectCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoriesIds = request.Vm.CategoryIds.Distinct();
        var projectId = request.ProjectId;

        var existingProject = await projectQueries.GetByIdAsync(projectId, cancellationToken);
        if (existingProject == null)
        {
            return ServiceResponse.NotFound($"Project with id {projectId} does not exist");
        }
        
        var userId = await userProvider.GetUserId();
        var userRole = userProvider.GetUserRole();

        if (existingProject.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
        {
            return ServiceResponse.Forbidden("You do not have permission to edit this entity");
        }
        
        existingProject.Categories.Clear();

        foreach (var categId in categoriesIds)
        {
            var existingCategory = await categoryQueries.GetByIdAsync(categId, cancellationToken);
            if (existingCategory == null)
            {
                return ServiceResponse.NotFound($"Category with id {categId} not found");
            }

            existingProject.Categories.Add(existingCategory);
        }

        try
        {
            await projectRepository.UpdateAsync(existingProject, cancellationToken);
            return ServiceResponse.Ok("Project categories updated successfully",
                mapper.Map<ProjectVM>(existingProject));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}