using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Services;
using BLL.ViewModels.Freelancer;
using Domain.Common.Interfaces;
using MediatR;

namespace BLL.Commands.Freelancers;

public class GetFreelancerByUserQuery : IRequest<ServiceResponse>
{
}

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
                return ServiceResponse.NotFoundResponse("Freelancer not found");
            }

            return ServiceResponse.OkResponse("Freelancer retrieved",
                mapper.Map<FreelancerVM>(freelancer));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalServerErrorResponse(exception.Message);
        }
    }
}