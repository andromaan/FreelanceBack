using AutoMapper;
using BLL.Common;
using BLL.Common.Interfaces;
using BLL.Services;
using Domain;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public class Update
{
    public record Command<TUpdateViewModel, TKey> : IRequest<ServiceResponse> where TUpdateViewModel : class
    {
        public required TKey Id { get; init; }
        public required TUpdateViewModel Model { get; init; }
    }

    public class CommandHandler<TUpdateViewModel, TViewModel, TEntity, TKey, TQueries>(
        IRepository<TEntity, TKey> repository,
        TQueries queries,
        IMapper mapper,
        IUserProvider userProvider)
        : IRequestHandler<Command<TUpdateViewModel, TKey>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TUpdateViewModel : class
        where TViewModel : class
        where TQueries : IQueries<TEntity, TKey>
    {
        public async Task<ServiceResponse> Handle(Command<TUpdateViewModel, TKey> request,
            CancellationToken cancellationToken)
        {
            var existingEntity = await queries.GetByIdAsync(request.Id, cancellationToken);

            if (existingEntity == null)
            {
                return ServiceResponse.NotFound($"{typeof(TEntity).Name} with ID {request.Id} not found");
            }
            
            if (existingEntity is AuditableEntity<TKey> auditable)
            {
                var userId = await userProvider.GetUserId();
                var userRole = userProvider.GetUserRole();

                if (auditable.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
                {
                    return ServiceResponse.Forbidden("You do not have permission to edit this entity");
                }
            }

            var entity = mapper.Map(request.Model, existingEntity);

            if (queries is IUniqueQuery<TEntity, TKey> uniqueQuery)
            {
                if (!await uniqueQuery.IsUniqueAsync(entity, cancellationToken))
                {
                    return ServiceResponse.BadRequest($"{typeof(TEntity).Name} with the same unique fields already exists");
                }
            }

            try
            {
                await repository.UpdateAsync(entity, cancellationToken);
                return ServiceResponse.Ok($"{typeof(TEntity).Name} updated", mapper.Map<TViewModel>(entity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalError(exception.Message);
            }
        }
    }
}