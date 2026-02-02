using FluentValidation;

namespace BLL.Commands.Employers.FluentValidations;

public class UpdateEmployerCommandValidator : AbstractValidator<UpdateEmployerCommand>
{
    public UpdateEmployerCommandValidator()
    {
        RuleFor(x => x.Vm.CompanyName)
            .NotEmpty().WithMessage("Company name is required");
        
        RuleFor(x => x.Vm.CompanyWebsite)
            .NotEmpty().WithMessage("Company website is required");
    }
}