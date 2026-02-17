using AutoMapper;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Interfaces.Repositories.Quotes;
using BLL.Services;
using BLL.ViewModels.Quote;
using MediatR;

namespace BLL.Commands.Quotes;

public record GetQuotesByProjectIdQuery : IRequest<ServiceResponse>
{
    public required Guid ProjectId { get; init; }
}

public class QueryHandler(
    IQuoteQueries quoteQueries,
    IProjectQueries projectQueries,
    IMapper mapper)
    : IRequestHandler<GetQuotesByProjectIdQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetQuotesByProjectIdQuery request,
        CancellationToken cancellationToken)
    {
        var existingProject = await projectQueries.GetByIdAsync(request.ProjectId, cancellationToken, true);
        if (existingProject == null)
        {
            return ServiceResponse.NotFound($"Project with id {request.ProjectId} not found");
        }

        var result = await quoteQueries.GetByProjectIdAsync(request.ProjectId, cancellationToken);
        return ServiceResponse.Ok("Quotes receive successfully",
            mapper.Map<List<QuoteVM>>(result));
    }
}