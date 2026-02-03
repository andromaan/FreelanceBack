using BLL.ViewModels.ContractMilestone;
using FluentValidation;

namespace BLL.Commands.ContractMilestones.FluentValidations;

public class UpdateContractMilestoneValidator : AbstractValidator<Update.Command<UpdateContractMilestoneVM, Guid>>
{
    public UpdateContractMilestoneValidator()
    {
        RuleFor(x => x.Model.Status).IsInEnum();
    }
}
