using BLL.Services;

namespace BLL.Common.Validators;

public interface ICreateValidator<TCreateViewModel>
    where TCreateViewModel : class
{
    Task<ServiceResponse?> ValidateAsync(
        TCreateViewModel createModel, 
        CancellationToken cancellationToken);
}