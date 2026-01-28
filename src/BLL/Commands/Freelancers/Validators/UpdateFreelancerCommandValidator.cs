using FluentValidation;

namespace BLL.Commands.Freelancers.Validators;

public class UpdateFreelancerCommandValidator : AbstractValidator<UpdateFreelancerCommand>
{
    public UpdateFreelancerCommandValidator()
    {
        RuleFor(x => x.Vm.Bio)
            .NotEmpty().WithMessage("Bio is required")
            .MaximumLength(1000).WithMessage("Bio must not exceed 1000 characters");

        RuleFor(x => x.Vm.HourlyRate)
            .GreaterThanOrEqualTo(0).WithMessage("Hourly rate must be non-negative");

        RuleFor(x => x.Vm.Location)
            .NotEmpty().WithMessage("Location is required")
            .MaximumLength(200).WithMessage("Location must not exceed 200 characters");
    }
}