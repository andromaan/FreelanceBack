using BLL.ViewModels.ContractMilestone;
using FluentValidation;

namespace BLL.Commands.ContractMilestones.FluentValidations;

public class
    UpdCMilestoneForEmployerCmdValidation : AbstractValidator<Update.Command<UpdContractMilestoneStatusEmployerVM, Guid>>
{
    public UpdCMilestoneForEmployerCmdValidation()
    {
        RuleFor(x => x.Model.Status).IsInEnum();
    }
}