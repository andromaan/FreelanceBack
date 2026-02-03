using BLL.ViewModels.Message;
using FluentValidation;

namespace BLL.Commands.Messages.FluentValidators;

public class CreateMessageWithoutContractValidator : AbstractValidator<Create.Command<CreateMessageWithoutContractVM>>
{
    public CreateMessageWithoutContractValidator()
    {
        RuleFor(m => m.Model.Text).NotEmpty().WithMessage("Text cannot be empty");
    }
}