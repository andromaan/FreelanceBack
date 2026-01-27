using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.ProjectMilestone;
using Domain;
using Domain.Models.Projects;
using MediatR;

namespace BLL.Commands.ProjectMilestones;

public record CreateProjectMilestoneCommand(CreateProjectMilestoneVM CreateVm) : IRequest<ServiceResponse>;

public class CreateProjectMilestoneCommandHandler(
    IProjectQueries projectQueries,
    IMapper mapper,
    IProjectMilestoneRepository projectMilestoneRepository,
    IUserProvider userProvider)
    : IRequestHandler<CreateProjectMilestoneCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateProjectMilestoneCommand request, CancellationToken cancellationToken)
    {
        var createVm = request.CreateVm;
        
        var userRole = userProvider.GetUserRole();
        var userId = await userProvider.GetUserId();
        
        var existingProject = await projectQueries.GetByIdAsync(createVm.ProjectId, cancellationToken);
        
        if (existingProject is null)
        {
            return ServiceResponse.NotFound($"Project with Id {createVm.ProjectId} not found");
        }
        
        if (existingProject.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
        {
            return ServiceResponse.Unauthorized("You are not authorized to create a milestone for this project");
        }

        var projectMilestone = mapper.Map<ProjectMilestone>(createVm);
        projectMilestone.Status = ProjectMilestoneStatus.Pending;

        try
        {
            var createProjectMilestone = await projectMilestoneRepository.CreateAsync(projectMilestone, cancellationToken);
            return ServiceResponse.Ok($"Project milestone created",
                mapper.Map<ProjectMilestoneVM>(createProjectMilestone));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message);
        }
    }
}