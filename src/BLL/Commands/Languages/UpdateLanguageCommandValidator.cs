using Domain.Models.Languages;
using FluentValidation;
using Domain.ViewModels.Language;

namespace BLL.Commands.Languages;

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
