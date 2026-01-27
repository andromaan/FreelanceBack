using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Quotes;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Quote;
using Domain.Models.Projects;
using MediatR;

namespace BLL.Commands.Quotes;

public record CreateQuoteCommand(CreateQuoteVM CreateVm) : IRequest<ServiceResponse>;

public class CreateQuoteCommandHandler(
    IProjectQueries projectQueries,
    IMapper mapper,
    IQuoteRepository quoteRepository,
    IUserProvider userProvider,
    IFreelancerQueries freelancerQueries)
    : IRequestHandler<CreateQuoteCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
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

        var quote = mapper.Map<Quote>(createVm);
        quote.FreelancerId = existingFreelancer.Id;

        try
        {
            var createQuote = await quoteRepository.CreateAsync(quote, cancellationToken);
            return ServiceResponse.Ok($"Quote created",
                mapper.Map<QuoteVM>(createQuote));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message);
        }
    }
}