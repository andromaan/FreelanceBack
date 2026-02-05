using BLL.Services;

namespace BLL.Common.Validators;

public interface IUpdateValidator<TEntity, TUpdateViewModel>
    where TEntity : class
    where TUpdateViewModel : class
{
    Task<ServiceResponse?> ValidateAsync(
        TEntity existingMilestone, 
        TUpdateViewModel updateModel, 
        CancellationToken cancellationToken);
}