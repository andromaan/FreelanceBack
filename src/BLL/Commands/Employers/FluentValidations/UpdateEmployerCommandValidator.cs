using BLL.ViewModels.Employer;
using FluentValidation;

namespace BLL.Commands.Employers.FluentValidations;

public class UpdateEmployerCommandValidator : AbstractValidator<UpdateByUser.Command<UpdateEmployerVM>>
{
    public UpdateEmployerCommandValidator()
    {
        RuleFor(x => x.Model.CompanyName)
            .NotEmpty().WithMessage("Company name is required");
        
        RuleFor(x => x.Model.CompanyWebsite)
            .NotEmpty().WithMessage("Company website is required");
    }
}