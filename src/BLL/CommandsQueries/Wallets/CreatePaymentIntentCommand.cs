using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Services;
using BLL.Services.StripeService;
using BLL.ViewModels.Wallet;
using MediatR;

namespace BLL.CommandsQueries.Wallets;

/// <summary>
/// Creates a Stripe PaymentIntent for depositing money into the current user's wallet.
/// Returns the clientSecret (needed by the frontend to confirm payment via Stripe.js)
/// and the paymentIntentId (needed to call ConfirmDepositCommand after payment is done).
/// </summary>
public record CreatePaymentIntentCommand(CreatePaymentIntentVM Vm) : IRequest<ServiceResponse>;

public class CreatePaymentIntentCommandHandler(
    IStripeService stripeService,
    IUserProvider userProvider,
    IUserWalletQueries userWalletQueries,
    IUserQueries userQueries)
    : IRequestHandler<CreatePaymentIntentCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(
        CreatePaymentIntentCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = await userProvider.GetUserId(cancellationToken);
            
            var user = await userQueries.GetByIdAsync(userId, cancellationToken);

            // Ensure wallet exists for this user
            var userWallet = await userWalletQueries.GetByUserIdAsync(userId, cancellationToken);
            if (userWallet is null)
                return ServiceResponse.NotFound("Wallet not found for current user.");
            
            if (user!.StripeCustomerId is null)
                return ServiceResponse.BadRequest("User does not have a Stripe customer ID.");

            var paymentIntent = await stripeService.CreatePaymentIntentAsync(
                request.Vm.Amount,
                request.Vm.Currency,
                user.StripeCustomerId,
                cancellationToken);

            return ServiceResponse.Ok("PaymentIntent created successfully.", new
            {
                PaymentIntentId = paymentIntent.Id,
                paymentIntent.ClientSecret,
                request.Vm.Amount,
                request.Vm.Currency
            });
        }
        catch (Exception ex)
        {
            return ServiceResponse.InternalError(ex.Message);
        }
    }
}
