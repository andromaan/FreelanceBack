using Domain.Models.Languages;
using Domain.ViewModels.Language;
using FluentValidation;

namespace BLL.Commands.Languages;

public class CreateLanguageCommandValidator : AbstractValidator<Create.Command<CreateLanguageVM, Language, int>>
{
    public CreateLanguageCommandValidator()
    {
        RuleFor(x => x.Model.Name)
            .NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Model.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}

