using BLL.ViewModels.Language;
using FluentValidation;

namespace BLL.Commands.Languages.Validators;

public class CreateLanguageCommandValidator : AbstractValidator<Create.Command<CreateLanguageVM>>
{
    public CreateLanguageCommandValidator()
    {
        RuleFor(x => x.Model.Name)
            .NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Model.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}

