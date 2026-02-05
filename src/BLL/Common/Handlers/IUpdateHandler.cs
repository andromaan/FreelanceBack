using BLL.Services;

namespace BLL.Common.Handlers;

/// <summary>
/// Unified handler for Update operations that combines validation and processing logic.
/// Returns ServiceResponse on validation/logic failure, or the processed entity on success.
/// </summary>
public interface IUpdateHandler<TEntity, TUpdateViewModel>
    where TEntity : class
    where TUpdateViewModel : class
{
    /// <summary>
    /// Handles validation and processing for entity update.
    /// </summary>
    /// <param name="existingEntity">The existing entity from database</param>
    /// <param name="mappedEntity">The entity after mapping with update data</param>
    /// <param name="updateModel">The update view model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Result&lt;TEntity, ServiceResponse&gt; where:
    /// - Success case contains the processed entity
    /// - Failure case contains ServiceResponse with error details
    /// </returns>
    Task<Result<TEntity, ServiceResponse>> HandleAsync(
        TEntity existingEntity,
        TEntity mappedEntity,
        TUpdateViewModel updateModel,
        CancellationToken cancellationToken);
}
