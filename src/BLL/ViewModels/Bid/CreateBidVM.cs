namespace BLL.ViewModels.Bid;

public class CreateBidVM
{
    public Guid ProjectId { get; set; }
    public decimal Amount { get; set; }
    public string? Message { get; set; }
}