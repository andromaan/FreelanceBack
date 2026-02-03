using AutoMapper;
using BLL.Common.Interfaces.Repositories.Messages;
using BLL.Services;
using BLL.ViewModels.Project;
using MediatR;

namespace BLL.Commands.Contracts;

public record GetMessagesByUserQuery : IRequest<ServiceResponse>;

public class QueryHandler(IMessageQueries messageQueries, IMapper mapper)
    : IRequestHandler<GetMessagesByUserQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetMessagesByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var projects = await messageQueries.GetByUserAsync(cancellationToken);

            return ServiceResponse.Ok("Projects retrieved", mapper.Map<List<ProjectVM>>(projects));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}