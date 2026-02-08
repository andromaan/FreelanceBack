using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Reviews;
using BLL.Services;
using BLL.ViewModels.Reviews;
using MediatR;

namespace BLL.Commands.Reviews;

public record GetByReviewerQuery : IRequest<ServiceResponse>;

public class GetReviewedUserQueryHandler(
    IReviewQueries reviewQueries,
    IMapper mapper,
    IUserProvider userProvider)
    : IRequestHandler<GetByReviewerQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetByReviewerQuery request, CancellationToken cancellationToken)
    {
        var userId = await userProvider.GetUserId();

        try
        {
            var reviews = await reviewQueries.GetReviewsByReviewerUser(userId, cancellationToken);

            return ServiceResponse.Ok("Reviews retrieved", mapper.Map<List<ReviewVM>>(reviews));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}