using AutoMapper;
using BLL.Common.Interfaces.Repositories.Disputes;
using BLL.Services;
using BLL.ViewModels.Dispute;
using MediatR;

namespace BLL.Commands.Disputes;

public record GetDisputesByUserQuery : IRequest<ServiceResponse>;

public class QueryHandler(IDisputeQueries disputeQueries, IMapper mapper)
    : IRequestHandler<GetDisputesByUserQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetDisputesByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var disputes = await disputeQueries.GetDisputesByUser(cancellationToken);

            return ServiceResponse.Ok("Disputes retrieved", mapper.Map<List<DisputeVM>>(disputes));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}