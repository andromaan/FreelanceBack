using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class GetById
{
    public record Query<TEntity, TKey, TViewModel> : IRequest<ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class
    {
        public required TKey Id { get; init; }
    }

    public class QueryHandler<TEntity, TKey, TViewModel>(IQueries<TEntity, TKey> queries, IMapper mapper)
        : IRequestHandler<Query<TEntity, TKey, TViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class
    {
        public async Task<ServiceResponse> Handle(Query<TEntity, TKey, TViewModel> request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await queries.GetByIdAsync(request.Id, cancellationToken);
                if (entity == null)
                {
                    return ServiceResponse.NotFoundResponse($"{typeof(TEntity).Name} not found");
                }
                var viewModel = mapper.Map<TViewModel>(entity);
                return ServiceResponse.OkResponse($"{typeof(TEntity).Name} retrieved", viewModel);
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalServerErrorResponse(exception.Message);
            }
        }
    }
}