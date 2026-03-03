using BLL.Models;
using Microsoft.Extensions.Options;
using Stripe;

namespace BLL.Services.StripeService;

public class StripeService : IStripeService
{
    private readonly PaymentIntentService _paymentIntentService;

    public StripeService(IOptions<StripeModel> stripeOptions)
    {
        StripeConfiguration.ApiKey = stripeOptions.Value.SecretKey;
        _paymentIntentService = new PaymentIntentService();
    }

    public async Task<PaymentIntent> CreatePaymentIntentAsync(
        decimal amount,
        string currency,
        string customerId,
        CancellationToken cancellationToken = default)
    {
        // Stripe expects amount in smallest currency unit (cents)
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100),
            Currency = currency.ToLower(),
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true
            }, 
            Customer = customerId
        };

        return await _paymentIntentService.CreateAsync(options, cancellationToken: cancellationToken);
    }

    public async Task<PaymentIntent> GetPaymentIntentAsync(
        string paymentIntentId,
        CancellationToken cancellationToken = default)
    {
        return await _paymentIntentService.GetAsync(paymentIntentId, cancellationToken: cancellationToken);
    }
}
