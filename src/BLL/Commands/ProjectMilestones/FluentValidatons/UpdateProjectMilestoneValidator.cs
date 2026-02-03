using BLL.ViewModels.ProjectMilestone;
using FluentValidation;

namespace BLL.Commands.ProjectMilestones.FluentValidatons;

public class UpdateProjectMilestoneValidator : AbstractValidator<Update.Command<UpdateProjectMilestoneVM, Guid>>
{
    public UpdateProjectMilestoneValidator()
    {
        RuleFor(x => x.Model.Status).IsInEnum();
    }
}
