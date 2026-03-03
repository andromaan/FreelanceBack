using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels.Wallet;

public class ConfirmDepositVM
{
    /// <summary>The Stripe PaymentIntent ID returned from the create-payment-intent endpoint.</summary>
    [Required]
    public string PaymentIntentId { get; set; } = string.Empty;
}
