using FluentValidation;

namespace BLL.Commands.UserLanguages.FluentValidations;

public class UpdateUserLanguageValidation : AbstractValidator<UpdateUserLanguageCommand>
{
    public UpdateUserLanguageValidation()
    {
        RuleFor(x => x.UpdateModel.ProficiencyLevel).IsInEnum();
    }
}