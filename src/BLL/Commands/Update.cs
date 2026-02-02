using AutoMapper;
using BLL.Common;
using BLL.Common.Interfaces;
using BLL.Common.Validators;
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
        IEnumerable<IUpdateValidator<TEntity, TUpdateViewModel>> validators) // Додано validators
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
            // 1. Перевірка існування entity
            var existingEntity = await queries.GetByIdAsync(request.Id, cancellationToken);

            if (existingEntity == null)
            {
                return ServiceResponse.NotFound(
                    $"{typeof(TEntity).Name} with ID {request.Id} not found");
            }
            
            // 2. Перевірка прав доступу (auditable)
            if (existingEntity is AuditableEntity<TKey> auditable)
            {
                var userId = await userProvider.GetUserId();
                var userRole = userProvider.GetUserRole();

                if (auditable.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
                {
                    return ServiceResponse.Forbidden(
                        "You do not have permission to edit this entity");
                }
            }

            // 3. ВИКОНАННЯ CUSTOM VALIDATORS (нова логіка)
            foreach (var validator in validators)
            {
                var validationResult = await validator.ValidateAsync(
                    existingEntity, 
                    request.Model, 
                    cancellationToken);
                    
                if (validationResult != null && !validationResult.Success)
                {
                    return validationResult;
                }
            }

            // 4. Mapping
            var entity = mapper.Map(request.Model, existingEntity);

            // 5. Перевірка унікальності
            if (queries is IUniqueQuery<TEntity, TKey> uniqueQuery)
            {
                if (!await uniqueQuery.IsUniqueAsync(entity, cancellationToken))
                {
                    return ServiceResponse.BadRequest(
                        $"{typeof(TEntity).Name} with the same unique fields already exists");
                }
            }

            // 6. Збереження
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