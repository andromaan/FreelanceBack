using AutoMapper;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Project;
using MediatR;

namespace BLL.Commands.Projects;

public record GetProjectsByEmployerQuery : IRequest<ServiceResponse>;

public class QueryHandler(IProjectQueries projectQueries, IMapper mapper)
    : IRequestHandler<GetProjectsByEmployerQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetProjectsByEmployerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var projects = await projectQueries.GetByEmployer(cancellationToken);

            return ServiceResponse.Ok("Projects retrieved", mapper.Map<List<ProjectVM>>(projects));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}