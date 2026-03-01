using FluentValidation;

namespace BLL.CommandsQueries.UserLanguages.FluentValidations;

public class CreateUserLanguageValidation : AbstractValidator<CreateUserLanguageCommand>
{
    public CreateUserLanguageValidation()
    {
        RuleFor(x => x.CreateModel.ProficiencyLevel).IsInEnum();
    }
}