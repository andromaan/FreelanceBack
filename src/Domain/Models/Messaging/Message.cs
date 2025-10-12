using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;
using Domain.Models.Freelance;

namespace Domain.Models.Messaging;

public class Message : AuditableEntity<Guid>
{
    public required Guid ContractId { get; set; }
    public Contract? Contract { get; set; }

    public required Guid SenderId { get; set; }
    public User? Sender { get; set; }

    public required string Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}