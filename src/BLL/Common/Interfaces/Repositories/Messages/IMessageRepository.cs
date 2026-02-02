using Domain.Models.Messaging;

namespace BLL.Common.Interfaces.Repositories.Messages;

public interface IMessageRepository : IRepository<Message, Guid>
{
    
}

