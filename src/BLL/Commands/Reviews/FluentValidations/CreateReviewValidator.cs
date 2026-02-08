using BLL.ViewModels.Reviews;
using FluentValidation;

namespace BLL.Commands.Reviews.FluentValidations;

public class CreateReviewValidation : AbstractValidator<Create.Command<CreateReviewVM>>
{
    public CreateReviewValidation()
    {
        RuleFor(r=> r.Model.ReviewText)
            .NotEmpty()
            .WithMessage("The review text cannot be empty");
        
        RuleFor(r=> r.Model.Rating)
            .InclusiveBetween(0.0m, 5.0m)
            .WithMessage("The rating must be between 0.0 and 5.0.");
    }
}