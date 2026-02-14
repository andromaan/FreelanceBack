using AutoMapper;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Portfolios;
using BLL.Services;
using BLL.ViewModels.Portfolio;
using MediatR;

namespace BLL.Commands.Portfolios;

public record GetPortfoliosByFreelancerIdQuery(Guid FreelancerId) : IRequest<ServiceResponse>;

public class GetPortfoliosByFreelancerIdQueryQueryHandler(
    IFreelancerQueries queriesFreelancer,
    IMapper mapper,
    IPortfolioQueries queriesPortfolio)
    : IRequestHandler<GetPortfoliosByFreelancerIdQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetPortfoliosByFreelancerIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var freelancer = await queriesFreelancer.GetByIdAsync(request.FreelancerId, cancellationToken);
            if (freelancer == null)
            {
                return ServiceResponse.NotFound("Freelancer not found");
            }
            
            var portfolios = await queriesPortfolio.GetByFreelancerIdAsync(request.FreelancerId, cancellationToken);

            return ServiceResponse.Ok("Portfolio's retrieved",
                mapper.Map<List<PortfolioVM>>(portfolios));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}