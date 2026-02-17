using AutoMapper;
using BLL.Common.Interfaces.Repositories.Reviews;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.ViewModels.Reviews;
using MediatR;

namespace BLL.Commands.Reviews;

public record GetByReviewedUserQuery : IRequest<ServiceResponse>
{
    public string ReviewedUserEmail { get; init; } = string.Empty;
}

public class GetByReviewedUserQueryQueryHandler(IReviewQueries reviewQueries, IUserQueries userQueries, IMapper mapper)
    : IRequestHandler<GetByReviewedUserQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetByReviewedUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userQueries.GetByEmailAsync(request.ReviewedUserEmail, cancellationToken);
        if (user is null)
        {
            return ServiceResponse.NotFound($"User with email {request.ReviewedUserEmail} not found");
        }

        try
        {
            var reviews = await reviewQueries.GetReviewsByReviewedUser(user.Id, cancellationToken);

            return ServiceResponse.Ok("Reviews retrieved", mapper.Map<List<ReviewVM>>(reviews));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}