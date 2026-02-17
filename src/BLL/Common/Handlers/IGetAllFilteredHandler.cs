using BLL.Services;

namespace BLL.Common.Handlers;

public interface IGetAllFilteredHandler<TEntity, in TFilterViewModel>
    where TEntity : class
    where TFilterViewModel : class
{
    Task<(ServiceResponse response, int? filteredTotalCount, List<TEntity>? filteredEntities)> HandleAsync(
        List<TEntity> entities,
        TFilterViewModel filter,
        CancellationToken cancellationToken);
}