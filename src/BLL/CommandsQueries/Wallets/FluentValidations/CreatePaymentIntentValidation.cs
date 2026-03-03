using FluentValidation;

namespace BLL.CommandsQueries.Wallets.FluentValidations;

public class CreatePaymentIntentValidation : AbstractValidator<CreatePaymentIntentCommand>
{
    public CreatePaymentIntentValidation()
    {
        RuleFor(p => p.Vm.Amount).InclusiveBetween(1, 100_000)
            .WithMessage("Amount must be between 1 and 100 000.")
            .PrecisionScale(18, 2, true)
            .WithMessage("Amount must have 2 decimal places.");
        
        RuleFor(p => p.Vm.Currency).NotEmpty().Length(3)
            .WithMessage("Currency must be a 3-letter ISO code, e.g. '");
    }
}