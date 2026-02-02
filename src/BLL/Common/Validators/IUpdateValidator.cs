using BLL.Services;

namespace BLL.Common.Validators;

public interface IUpdateValidator<TEntity, TUpdateViewModel>
    where TEntity : class
    where TUpdateViewModel : class
{
    Task<ServiceResponse?> ValidateAsync(
        TEntity existingEmployer, 
        TUpdateViewModel updateModel, 
        CancellationToken cancellationToken);
}