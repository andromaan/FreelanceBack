using AutoMapper;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.ViewModels.Freelancer;
using MediatR;

namespace BLL.Commands.Freelancers;

public record GetFreelancerByEmailQuery(string Email) : IRequest<ServiceResponse>;

public class GetFreelancerByEmailQueryHandler(
    IFreelancerQueries queriesFreelancer,
    IMapper mapper,
    IUserQueries userQueries)
    : IRequestHandler<GetFreelancerByEmailQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetFreelancerByEmailQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userQueries.GetByEmailAsync(request.Email, cancellationToken);
            
            if (user == null)
            {
                return ServiceResponse.NotFound("User not found with the provided email");
            }

            var freelancer = await queriesFreelancer.GetByUserIdAsync(user.Id, cancellationToken);
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