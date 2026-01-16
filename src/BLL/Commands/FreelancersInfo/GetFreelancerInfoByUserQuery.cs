using AutoMapper;
using BLL.Common.Interfaces.Repositories.FreelancersInfo;
using BLL.Services;
using Domain.Common.Interfaces;
using Domain.ViewModels.FreelancerInfo;
using MediatR;

namespace BLL.Commands.FreelancersInfo;

public class GetFreelancerInfoByUserQuery : IRequest<ServiceResponse>
{
}

public class QueryHandler(
    IFreelancerInfoQueries queriesFreelancerInfo,
    IUserProvider userProvider,
    IMapper mapper)
    : IRequestHandler<GetFreelancerInfoByUserQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetFreelancerInfoByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await userProvider.GetUserId();

            var freelancerInfo = await queriesFreelancerInfo.GetByUserId(userId, cancellationToken, includes: true);
            if (freelancerInfo == null)
            {
                return ServiceResponse.NotFoundResponse("Freelancer info not found");
            }

            return ServiceResponse.OkResponse("Freelancer info retrieved",
                mapper.Map<FreelancerInfoVM>(freelancerInfo));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalServerErrorResponse(exception.Message);
        }
    }
}