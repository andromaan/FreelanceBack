namespace BLL.ViewModels.Reviews;

public class CreateReviewVM
{
    public Guid ContractId { get; set; }
    public decimal Rating { get; set; }
    public string ReviewText { get; set; } = string.Empty;
}