using AutoMapper;
using BLL.Common.Interfaces.Repositories.Bids;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Bid;
using MediatR;

namespace BLL.Commands.Bids;

public record GetBidsByProjectIdQuery : IRequest<ServiceResponse>
{
    public required Guid ProjectId { get; init; }
}

public class QueryHandler(
    IBidQueries bidQueries,
    IProjectQueries projectQueries,
    IMapper mapper)
    : IRequestHandler<GetBidsByProjectIdQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetBidsByProjectIdQuery request,
        CancellationToken cancellationToken)
    {
        var existingProject = await projectQueries.GetByIdAsync(request.ProjectId, cancellationToken, true);
        if (existingProject == null)
        {
            return ServiceResponse.NotFound($"Project with id {request.ProjectId} not found");
        }

        var result = await bidQueries.GetByProjectIdAsync(request.ProjectId, cancellationToken);
        return ServiceResponse.Ok("Bids receive successfully",
            mapper.Map<List<BidVM>>(result));
    }
}