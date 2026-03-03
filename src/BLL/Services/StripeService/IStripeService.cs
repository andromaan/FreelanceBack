using Stripe;

namespace BLL.Services.StripeService;

public interface IStripeService
{
    /// <summary>
    /// Creates a Stripe PaymentIntent for the given amount and currency.
    /// Returns the PaymentIntent object (contains ClientSecret and Id).
    /// </summary>
    Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, string currency, string customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an existing PaymentIntent by its ID from Stripe.
    /// </summary>
    Task<PaymentIntent> GetPaymentIntentAsync(string paymentIntentId, CancellationToken cancellationToken = default);
}
