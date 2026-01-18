using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class GetAll
{
    public record Query<TViewModel> : IRequest<ServiceResponse> where TViewModel : class;

    public class QueryHandler<TEntity, TKey, TViewModel>(IQueries<TEntity, TKey> queries, IMapper mapper)
        : IRequestHandler<Query<TViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class
    {
        public async Task<ServiceResponse> Handle(Query<TViewModel> request, CancellationToken cancellationToken)
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