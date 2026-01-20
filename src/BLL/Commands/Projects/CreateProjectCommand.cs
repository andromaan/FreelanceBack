using AutoMapper;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Project;
using Domain.Models.Projects;
using MediatR;

namespace BLL.Commands.Projects;

public record CreateProjectCommand(CreateProjectVM createVM) : IRequest<ServiceResponse>
{
}

public class CreateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
    : IRequestHandler<CreateProjectCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var createVM = request.createVM;
        
        var project = mapper.Map<Project>(createVM);
        project.Status = ProjectStatus.Open;
        
        try
        {
            var createProject = await repository.CreateAsync(project, cancellationToken);
            return ServiceResponse.Ok($"Project created",
                mapper.Map<ProjectVM>(createProject));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message );
        }
    }
}