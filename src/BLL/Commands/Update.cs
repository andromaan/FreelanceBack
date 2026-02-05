using AutoMapper;
using BLL.Common;
using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Services;
using Domain;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public class Update
{
    public record Command<TUpdateViewModel, TKey> : IRequest<ServiceResponse>
        where TUpdateViewModel : class
    {
        public required TKey Id { get; init; }
        public required TUpdateViewModel Model { get; init; }
    }

    public class CommandHandler<TUpdateViewModel, TViewModel, TEntity, TKey, TQueries>(
        IRepository<TEntity, TKey> repository,
        TQueries queries,
        IMapper mapper,
        IUserProvider userProvider,
        IEnumerable<IUpdateHandler<TEntity, TUpdateViewModel>> handlers)
        : IRequestHandler<Command<TUpdateViewModel, TKey>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TUpdateViewModel : class
        where TViewModel : class
        where TQueries : IQueries<TEntity, TKey>
    {
        public async Task<ServiceResponse> Handle(
            Command<TUpdateViewModel, TKey> request,
            CancellationToken cancellationToken)
        {
            // 1. Check entity existence
            var entity = await queries.GetByIdAsync(request.Id, cancellationToken, asNoTracking: true);

            if (entity == null)
            {
                return ServiceResponse.NotFound(
                    $"{typeof(TEntity).Name} with ID {request.Id} not found");
            }

            // 2. Check access rights (auditable)
            if (request.Model is not ISkipAuditable)
            {
                if (entity is AuditableEntity<TKey> auditable)
                {
                    var userId = await userProvider.GetUserId();
                    var userRole = userProvider.GetUserRole();

                    if (auditable.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
                    {
                        return ServiceResponse.Forbidden(
                            "You do not have permission to edit this entity");
                    }
                }
            }

            // 3. Mapping
            if (request.Model is not ISkipMapper)
            {
                mapper.Map(request.Model, entity);
            }
            
            // 4. Check uniqueness
            if (queries is IUniqueQuery<TEntity, TKey> uniqueQuery)
            {
                if (!await uniqueQuery.IsUniqueAsync(entity, cancellationToken))
                {
                    return ServiceResponse.BadRequest(
                        $"{typeof(TEntity).Name} with the same unique fields already exists");
                }
            }

            // 5. Execute new unified handlers (validation + processing in one place)
            foreach (var handler in handlers)
            {
                var result = await handler.HandleAsync(
                    entity,
                    request.Model,
                    cancellationToken);

                if (result is { Success: false })
                {
                    return result;
                }
            }

            // 6. Save to database
            try
            {
                await repository.UpdateAsync(entity, cancellationToken);
                return ServiceResponse.Ok(
                    $"{typeof(TEntity).Name} updated",
                    mapper.Map<TViewModel>(entity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalError(exception.Message);
            }
        }
    }
}