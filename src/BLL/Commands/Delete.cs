using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories;
using BLL.Services;
using Domain;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public class Delete
{
    // ReSharper disable once UnusedTypeParameter
    public record Command<TViewModel, TKey> : IRequest<ServiceResponse> where TViewModel : class
    {
        public required TKey Id { get; init; }
    }

    public class CommandHandler<TViewModel, TEntity, TKey>(
        IRepository<TEntity, TKey> repository,
        IQueries<TEntity, TKey> queries,
        IUserProvider userProvider)
        : IRequestHandler<Command<TViewModel, TKey>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class
    {
        public async Task<ServiceResponse> Handle(Command<TViewModel, TKey> request,
            CancellationToken cancellationToken)
        {
            var existingEntity = await queries.GetByIdAsync(request.Id, cancellationToken);
            
            if (existingEntity is null)
            {
                return ServiceResponse.NotFound($"{typeof(TEntity).Name} with ID {request.Id} not found");
            }

            if (existingEntity is AuditableEntity<TKey> auditable)
            {
                var userId = await userProvider.GetUserId();
                var userRole = userProvider.GetUserRole();

                if (auditable.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
                {
                    return ServiceResponse.Forbidden("You do not have permission to delete this entity");
                }
            }

            try
            {
                await repository.DeleteAsync(request.Id, cancellationToken);
                return ServiceResponse.Ok($"{typeof(TEntity).Name} deleted");
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalError(exception.Message);
            }
        }
    }
}