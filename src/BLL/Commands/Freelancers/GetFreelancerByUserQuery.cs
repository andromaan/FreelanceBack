using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Services;
using BLL.ViewModels.Freelancer;
using MediatR;

namespace BLL.Commands.Freelancers;

public record GetFreelancerByUserQuery : IRequest<ServiceResponse>;

public class QueryHandler(
    IFreelancerQueries queriesFreelancer,
    IUserProvider userProvider,
    IMapper mapper)
    : IRequestHandler<GetFreelancerByUserQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetFreelancerByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await userProvider.GetUserId();

            var freelancer = await queriesFreelancer.GetByUserId(userId, cancellationToken, includes: true);
            if (freelancer == null)
            {
                return ServiceResponse.NotFound("Freelancer not found");
            }

            return ServiceResponse.Ok("Freelancer retrieved",
                mapper.Map<FreelancerVM>(freelancer));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}