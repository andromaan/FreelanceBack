using AutoMapper;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.Contract;
using MediatR;

namespace BLL.Commands.Contracts;

public record GetContractByFreelancerIdQuery(Guid FreelancerId) : IRequest<ServiceResponse>;

public class GetContractByFreelancerIdQueryQueryHandler(IContractQueries contractQueries, IMapper mapper)
    : IRequestHandler<GetContractByFreelancerIdQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetContractByFreelancerIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var contracts = 
                await contractQueries.GetByFreelancerId(request.FreelancerId, cancellationToken);

            return ServiceResponse.Ok("Contracts by freelancer retrieved", 
                mapper.Map<List<ContractVM>>(contracts));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}