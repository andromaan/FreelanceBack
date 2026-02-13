using BLL.Common.Handlers;
using BLL.Services;
using BLL.Services.ImageService;
using Domain.Models.Users;

namespace BLL.Commands.Users.Handlers;

public class DeleteUserHandler(IImageService imageService) : IDeleteHandler<User>
{
    public Task<ServiceResponse?> HandleAsync(User entity, CancellationToken cancellationToken)
    {
        if (entity.AvatarImg != null)
        {
            var isAvatarDeleted = imageService.DeleteImage(Settings.ImagesPathSettings.UserAvatarImagesPath, entity.AvatarImg);
            if (!isAvatarDeleted)
            {
                return Task.FromResult<ServiceResponse?>(
                    ServiceResponse.InternalError("Failed to delete user avatar image."));
            }
        }

        return Task.FromResult<ServiceResponse?>(ServiceResponse.Ok());
    }
}