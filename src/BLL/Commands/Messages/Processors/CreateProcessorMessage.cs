using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Processors;
using BLL.ViewModels.Message;
using Domain.Models.Messaging;

namespace BLL.Commands.Messages.Processors;

public class CreateProcessorMessage(IUserQueries userQueries)
    : ICreateProcessor<Message, CreateMessageVM>
{
    public async Task<Message> ProcessAsync(Message entity, CreateMessageVM createVm, CancellationToken cancellationToken)
    {
        // Встановлюємо ReceiverId на основі email
        var receiver = await userQueries.GetByEmailAsync(createVm.ReceiverEmail, cancellationToken);
        entity.ReceiverId = receiver!.Id;

        return entity;
    }
}
