using BLL.ViewModels.ContractMilestone;
using FluentValidation;

namespace BLL.Commands.ContractMilestones.FluentValidations;

public class
    UpdCMilestoneForFreelancerCmdValidation : AbstractValidator<
    Update.Command<UpdContractMilestoneStatusFreelancerVM, Guid>>
{
    public UpdCMilestoneForFreelancerCmdValidation()
    {
        RuleFor(x => x.Model.Status).IsInEnum();
    }
}