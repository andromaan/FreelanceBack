using FluentValidation;

namespace BLL.Commands.UserLanguages.FluentValidations;

public class CreateUserLanguageValidation : AbstractValidator<CreateUserLanguageCommand>
{
    public CreateUserLanguageValidation()
    {
        RuleFor(x => x.CreateModel.ProficiencyLevel).IsInEnum();
    }
}