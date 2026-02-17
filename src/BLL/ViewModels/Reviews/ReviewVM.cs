namespace BLL.ViewModels.Reviews;

public class ReviewVM
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Guid ReviewedUserId { get; set; }
    public decimal Rating { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public string ReviewerRoleId { get; set; } = null!;
    public Guid ReviewerId { get; set; }
}