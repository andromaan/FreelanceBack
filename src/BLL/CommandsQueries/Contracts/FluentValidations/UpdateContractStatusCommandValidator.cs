using BLL.CommandsQueries.GenericCRUD.Update;
using BLL.ViewModels.Contract;
using FluentValidation;

namespace BLL.CommandsQueries.Contracts.FluentValidations;

public class UpdateContractStatusCommandValidator : AbstractValidator<Update.Command<UpdateContractStatusVM, Guid>>
{
    public UpdateContractStatusCommandValidator()
    {
        RuleFor(x => x.Model.Status)
            .IsInEnum();
    }
}
