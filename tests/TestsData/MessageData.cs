using Domain.Models.Messaging;

namespace TestsData;

public class MessageData
{
    public static Message CreateMessage(
        Guid? id = null, 
        Guid? contractId = null, 
        Guid? receiverId = null, 
        Guid? senderId = null,
        string? text = null)
    {
        return new Message
        {
            Id = id ?? Guid.NewGuid(),
            ContractId = contractId,
            ReceiverId = receiverId ?? Guid.NewGuid(),
            Text = text ?? "Test message text",
            SentAt = DateTime.UtcNow,
            CreatedBy = senderId ?? Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
    }
}
