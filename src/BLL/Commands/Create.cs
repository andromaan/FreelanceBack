using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class Create
{
    public record Command<TCreateViewModel> : IRequest<ServiceResponse> where TCreateViewModel : class
    {
        public required TCreateViewModel Model { get; init; }
    }

    public class
        CommandHandler<TCreateViewModel, TViewModel, TEntity, TKey>(
            IRepository<TEntity, TKey> repository,
            IMapper mapper)
        : IRequestHandler<Command<TCreateViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TCreateViewModel : class
        where TViewModel : class
    {
        public async Task<ServiceResponse> Handle(Command<TCreateViewModel> request,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<TEntity>(request.Model);
                var createdEntity = await repository.CreateAsync(entity, cancellationToken);
                return ServiceResponse.Ok($"{typeof(TEntity).Name} created",
                    mapper.Map<TViewModel>(createdEntity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalError(exception.Message);
            }
        }
    }

    public class
        CommandHandlerUniqueCheck<TCreateViewModel, TViewModel, TEntity, TKey, TQueries>(
            IRepository<TEntity, TKey> repository,
            IMapper mapper,
            TQueries query)
        : IRequestHandler<Command<TCreateViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TCreateViewModel : class
        where TViewModel : class
        where TQueries : IUniqueQuery<TEntity, TKey>, IQueries<TEntity, TKey>
    {
        public async Task<ServiceResponse> Handle(Command<TCreateViewModel> request,
            CancellationToken cancellationToken)
        {
            var entity = mapper.Map<TEntity>(request.Model);

            if (!await query.IsUniqueAsync(entity, cancellationToken))
            {
                return ServiceResponse.BadRequest($"{typeof(TEntity).Name} with the same unique fields already exists");
            }

            try
            {
                var createdEntity = await repository.CreateAsync(entity, cancellationToken);
                return ServiceResponse.Ok($"{typeof(TEntity).Name} created",
                    mapper.Map<TViewModel>(createdEntity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalError(exception.Message);
            }
        }
    }
}