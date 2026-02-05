using FluentValidation;

namespace BLL.Commands.ContractMilestones.FluentValidations;

public class UpdCMilestoneForFreelancerCmdValidation : AbstractValidator<UpdCMilestoneForFreelancerCmd>
{
    public UpdCMilestoneForFreelancerCmdValidation()
    {
        RuleFor(x => x.Status).IsInEnum();
    }
}