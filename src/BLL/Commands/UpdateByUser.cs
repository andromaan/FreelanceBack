using AutoMapper;
using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public class UpdateByUser
{
    public record Command<TUpdateViewModel> : IRequest<ServiceResponse>
        where TUpdateViewModel : class
    {
        public required TUpdateViewModel Model { get; init; }
    }

    public class CommandHandler<TUpdateViewModel, TViewModel, TEntity, TKey, TQueries>(
        IRepository<TEntity, TKey> repository,
        TQueries queries,
        IMapper mapper,
        IUserProvider userProvider,
        IEnumerable<IUpdateHandler<TEntity, TUpdateViewModel>> handlers)
        : IRequestHandler<Command<TUpdateViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TUpdateViewModel : class
        where TViewModel : class
        where TQueries : IQueries<TEntity, TKey>, IByUserQuery<TEntity, TKey>
    {
        public async Task<ServiceResponse> Handle(
            Command<TUpdateViewModel> request,
            CancellationToken cancellationToken)
        {
            var userId = await userProvider.GetUserId();
            
            // 1. Check entity existence
            var existingEntity = await queries.GetByUser(userId, cancellationToken);

            if (existingEntity == null)
            {
                return ServiceResponse.NotFound(
                    $"{typeof(TEntity).Name} not found by user id {userId}");
            }

            // 2. Mapping
            if (request.Model is not ISkipMapper)
            {
                mapper.Map(request.Model, existingEntity);
            }

            // 3. Execute new unified handlers (validation + processing in one place)
            foreach (var handler in handlers)
            {
                var result = await handler.HandleAsync(
                    existingEntity,
                    request.Model,
                    cancellationToken);

                if (result is { Success: false })
                {
                    return result;
                }
            }

            // 4. Save to database
            try
            {
                await repository.UpdateAsync(existingEntity, cancellationToken);
                return ServiceResponse.Ok(
                    $"{typeof(TEntity).Name} updated",
                    mapper.Map<TViewModel>(existingEntity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalError(exception.Message);
            }
        }
    }
}