namespace BLL.ViewModels.Wallet;

public class CreatePaymentIntentVM
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
}
