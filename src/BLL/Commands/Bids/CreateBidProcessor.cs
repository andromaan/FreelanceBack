using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Processors;
using BLL.ViewModels.Bid;
using Domain.Models.Projects;

namespace BLL.Commands.Bids;

public class CreateBidProcessor(
    IUserProvider userProvider,
    IFreelancerQueries freelancerQueries)
    : ICreateProcessor<Bid, CreateBidVM>
{
    public async Task<Bid> ProcessAsync(Bid entity, CreateBidVM createVm, CancellationToken cancellationToken)
    {
        var userId = await userProvider.GetUserId();

        var existingFreelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);
        entity.FreelancerId = existingFreelancer!.Id;

        return entity;
    }
}