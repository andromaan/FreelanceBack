using BLL.ViewModels.Contract;
using FluentValidation;

namespace BLL.Commands.Contracts.FluentValidations;

public class UpdateContractStatusCommandValidator : AbstractValidator<Update.Command<UpdateContractStatusVM, Guid>>
{
    public UpdateContractStatusCommandValidator()
    {
        RuleFor(x => x.Model.Status)
            .IsInEnum();
    }
}
