using BLL.CommandsQueries.GenericCRUD.Update;
using BLL.ViewModels.ProjectMilestone;
using FluentValidation;

namespace BLL.CommandsQueries.ProjectMilestones.FluentValidations;

public class UpdateProjectMilestoneValidator : AbstractValidator<Update.Command<UpdateProjectMilestoneVM, Guid>>
{
    public UpdateProjectMilestoneValidator()
    {
        RuleFor(x => x.Model.Status).IsInEnum();
    }
}
