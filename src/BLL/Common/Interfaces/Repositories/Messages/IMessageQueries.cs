using Domain.Models.Messaging;

namespace BLL.Common.Interfaces.Repositories.Messages;

public interface IMessageQueries : IQueries<Message, Guid>
{
}