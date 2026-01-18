using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class Delete
{
    public record Command<TViewModel, TKey> : IRequest<ServiceResponse> where TViewModel : class
    {
        public required TKey Id { get; init; }
    }

    public class CommandHandler<TEntity, TKey>(IRepository<TEntity, TKey> repository, IQueries<TEntity, TKey> queries)
        : IRequestHandler<Command<TEntity, TKey>, ServiceResponse>
        where TEntity : Entity<TKey>
    {
        public async Task<ServiceResponse> Handle(Command<TEntity, TKey> request, CancellationToken cancellationToken)
        {
            if (await queries.GetByIdAsync(request.Id, cancellationToken, true) is null)
            {
                return ServiceResponse.NotFoundResponse($"{typeof(TEntity).Name} with ID {request.Id} not found");
            }

            try
            {
                await repository.DeleteAsync(request.Id, cancellationToken);
                return ServiceResponse.OkResponse($"{typeof(TEntity).Name} deleted");
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalServerErrorResponse(exception.Message);
            }
        }
    }
}