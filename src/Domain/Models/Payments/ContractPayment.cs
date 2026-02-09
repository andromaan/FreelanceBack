using Domain.Common.Abstractions;
using Domain.Models.Contracts;

namespace Domain.Models.Payments;

public class ContractPayment : Entity<Guid>
{
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
    
    public Guid MilestoneId { get; set; }
    public ContractMilestone? Milestone { get; set; }
    
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // TODO enum
}