using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.Message;

namespace BLL.Commands.Messages.Validators;

public class CreateValidatorMessageWithoutContract(IUserQueries userQueries, IUserProvider userProvider)
    : ICreateValidator<CreateMessageWithoutContractVM>
{
    public async Task<ServiceResponse?> ValidateAsync(CreateMessageWithoutContractVM createModel,
        CancellationToken cancellationToken)
    {
        var senderId = await userProvider.GetUserId();

        var receiver = await userQueries.GetByEmailAsync(createModel.ReceiverEmail, cancellationToken);
        if (receiver == null)
        {
            return ServiceResponse.BadRequest("Receiver with the specified email does not exist");
        }
        
        if (receiver.Id == senderId)
        {
            return ServiceResponse.BadRequest("Cannot send a message to yourself");
        }
        
        return null; // Validation passed
    }
}