using Domain.Models.Messaging;

namespace BLL.Common.Interfaces.Repositories.Messages;

public interface IMessageQueries : IQueries<Message, Guid>
{
    Task<List<Message>> GetByUserAsync(CancellationToken cancellationToken);
    Task<List<Message>> GetByContractAsync(Guid contractId, CancellationToken cancellationToken);
}