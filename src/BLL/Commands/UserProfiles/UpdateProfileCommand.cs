using AutoMapper;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.UserProfiles;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using Domain.ViewModels.UserProfile;
using MediatR;

namespace BLL.Commands.UserProfiles;

public record UpdateProfileCommand(Guid UserId, UpdateProfileVM Vm) : IRequest<ServiceResponse>
{
}

public class UpdateProfileCommandHandler(
    IUserQueries userQueries,
    IUserProfileRepository userProfileRepository,
    ILanguageQueries languageQueries,
    IMapper mapper)
    : IRequestHandler<UpdateProfileCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var vm = request.Vm;

        if (await userQueries.GetByIdAsync(userId, cancellationToken) == null)
        {
            return ServiceResponse.NotFoundResponse($"User with id {userId} not found");
        }
    }
}