using BLL.ViewModels.Language;
using FluentValidation;

namespace BLL.Commands.Languages.FluentValidations;

public class UpdateLanguageCommandValidator : AbstractValidator<Update.Command<CreateLanguageVM, int>>
{
    public UpdateLanguageCommandValidator()
    {
        RuleFor(x => x.Model.Name)
            .NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Model.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}
