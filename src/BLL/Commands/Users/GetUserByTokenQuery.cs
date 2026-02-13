using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.ViewModels.User;
using MediatR;

namespace BLL.Commands.Users;

public record GetUserByTokenQuery : IRequest<ServiceResponse>;

public class QueryHandler(IUserQueries userQueries, IMapper mapper, IUserProvider userProvider)
    : IRequestHandler<GetUserByTokenQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await userProvider.GetUserId();
            
            var user = await userQueries.GetByIdAsync(userId, cancellationToken);

            return ServiceResponse.Ok("Your profile retrieved", mapper.Map<UserVM>(user));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}