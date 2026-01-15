using Domain.Models.Languages;
using Domain.ViewModels.Language;
using FluentValidation;

namespace BLL.Commands.Languages.Validators;

public class UpdateLanguageCommandValidator : AbstractValidator<Update.Command<CreateLanguageVM, Language, int>>
{
    public UpdateLanguageCommandValidator()
    {
        RuleFor(x => x.Model.Name)
            .NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Model.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}
