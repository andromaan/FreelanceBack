using FluentValidation;

namespace BLL.Commands.Users.FluentValidations;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.CreateModel.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.CreateModel.Password)
            .NotEmpty().WithMessage("Password is required.")
            .NotNull().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.CreateModel.RoleId)
            .NotEmpty().WithMessage("Role ID is required.");
    }
}