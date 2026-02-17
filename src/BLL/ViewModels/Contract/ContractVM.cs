namespace BLL.ViewModels.Contract;

public class ContractVM
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid FreelancerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal AgreedRate { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid EmployerUserId { get; set; }
}