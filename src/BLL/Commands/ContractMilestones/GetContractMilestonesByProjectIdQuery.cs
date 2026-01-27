using AutoMapper;
using BLL.Commands.ContractMilestones;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using MediatR;

namespace BLL.Commands.ContractMilestones;

public record GetContractMilestonesByContractIdQuery : IRequest<ServiceResponse>
{
    public required Guid ContractId { get; init; }
}

public class QueryHandler(
    IContractMilestoneQueries contractMilestoneService,
    IContractQueries contractQueries,
    IMapper mapper)
    : IRequestHandler<GetContractMilestonesByContractIdQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetContractMilestonesByContractIdQuery request,
        CancellationToken cancellationToken)
    {
        var existingContract = await contractQueries.GetByIdAsync(request.ContractId, cancellationToken, true);
        if (existingContract == null)
        {
            return ServiceResponse.NotFound($"Contract with id {request.ContractId} not found");
        }

        var result = await contractMilestoneService.GetByContractIdAsync(request.ContractId, cancellationToken);
        return ServiceResponse.Ok("Contract milestones receive successfully",
            mapper.Map<List<ContractMilestoneVM>>(result));
    }
}