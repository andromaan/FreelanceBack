namespace BLL.Common.Processors;

public interface ICreateProcessor<TEntity, TCreateViewModel>
    where TEntity : class
    where TCreateViewModel : class
{
    Task<TEntity> ProcessAsync(
        TEntity entity, 
        TCreateViewModel createModel,
        CancellationToken cancellationToken);
}