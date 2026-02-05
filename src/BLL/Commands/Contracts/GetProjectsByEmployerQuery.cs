using AutoMapper;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.Contract;
using MediatR;

namespace BLL.Commands.Contracts;

public record GetContractByUserQuery : IRequest<ServiceResponse>;

public class QueryHandler(IContractQueries contractQueries, IMapper mapper)
    : IRequestHandler<GetContractByUserQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetContractByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var contracts = await contractQueries.GetByUser(cancellationToken);

            return ServiceResponse.Ok("Contracts retrieved", mapper.Map<List<ContractVM>>(contracts));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}