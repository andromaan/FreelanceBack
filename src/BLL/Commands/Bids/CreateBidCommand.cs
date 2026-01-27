using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Bids;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Bid;
using Domain.Models.Projects;
using MediatR;

namespace BLL.Commands.Bids;

public record CreateBidCommand(CreateBidVM CreateVm) : IRequest<ServiceResponse>;

public class CreateBidCommandHandler(
    IProjectQueries projectQueries,
    IMapper mapper,
    IBidRepository bidRepository,
    IUserProvider userProvider,
    IFreelancerQueries freelancerQueries)
    : IRequestHandler<CreateBidCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        var createVm = request.CreateVm;
        
        var existingProject = await projectQueries.GetByIdAsync(createVm.ProjectId, cancellationToken);
        
        if (existingProject is null)
        {
            return ServiceResponse.NotFound($"Project with Id {createVm.ProjectId} not found");
        }

        var userId = await userProvider.GetUserId();
        
        var existingFreelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);
        if (existingFreelancer is null)
        {
            return ServiceResponse.NotFound($"Freelancer not found by this user. User id {userId}");
        }

        var bid = mapper.Map<Bid>(createVm);
        bid.FreelancerId = existingFreelancer.Id;

        try
        {
            var createBid = await bidRepository.CreateAsync(bid, cancellationToken);
            return ServiceResponse.Ok($"Bid created",
                mapper.Map<BidVM>(createBid));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message);
        }
    }
}