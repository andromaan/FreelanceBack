namespace BLL.ViewModels.Quote;

public class CreateQuoteVM
{
    public Guid ProjectId { get; set; }
    public decimal Amount { get; set; }
    public string? Message { get; set; }
}