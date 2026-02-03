using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Processors;
using BLL.ViewModels.Message;
using Domain.Models.Messaging;

namespace BLL.Commands.Messages.Processors;

public class CreateProcessorMessageWithoutContract(
    IUserQueries userQueries
    )
    : ICreateProcessor<Message, CreateMessageWithoutContractVM>
{
    public async Task<Message> ProcessAsync(Message entity, CreateMessageWithoutContractVM createVm, CancellationToken cancellationToken)
    {
        var receiver = await userQueries.GetByEmailAsync(createVm.ReceiverEmail, cancellationToken);
        entity.ReceiverId = receiver!.Id;

        return entity;
    }
}