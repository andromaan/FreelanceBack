using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Processors;
using BLL.ViewModels.Quote;
using Domain.Models.Projects;

namespace BLL.Commands.Quotes;

public class CreateQuoteProcessor(
    IUserProvider userProvider,
    IFreelancerQueries freelancerQueries)
    : ICreateProcessor<Quote, CreateQuoteVM>
{
    public async Task<Quote> ProcessAsync(Quote entity, CreateQuoteVM createVm, CancellationToken cancellationToken)
    {
        var userId = await userProvider.GetUserId();

        var existingFreelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);
        entity.FreelancerId = existingFreelancer!.Id;

        return entity;
    }
}