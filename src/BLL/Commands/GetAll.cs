using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class GetAll
{
    public record Query<TEntity, TKey, TViewModel> : IRequest<ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class;

    public class QueryHandler<TEntity, TKey, TViewModel>(IQueries<TEntity, TKey> queries, IMapper mapper)
        : IRequestHandler<Query<TEntity, TKey, TViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class
    {
        public async Task<ServiceResponse> Handle(Query<TEntity, TKey, TViewModel> request, CancellationToken cancellationToken)
        {
            try
            {
                var entities = await queries.GetAllAsync(cancellationToken);
                var viewModels = mapper.Map<List<TViewModel>>(entities);
                return ServiceResponse.OkResponse($"{typeof(TEntity).Name}s retrieved", viewModels);
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalServerErrorResponse(exception.Message);
            }
        }
    }
}