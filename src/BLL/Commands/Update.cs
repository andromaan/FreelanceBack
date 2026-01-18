using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class Update
{
    public record Command<TUpdateViewModel, TKey> : IRequest<ServiceResponse> where TUpdateViewModel : class
    {
        public required TKey Id { get; init; }
        public required TUpdateViewModel Model { get; init; }
    }

    public class CommandHandler<TUpdateViewModel, TViewModel, TEntity, TKey>(
        IRepository<TEntity, TKey> repository,
        IQueries<TEntity, TKey> queries,
        IMapper mapper)
        : IRequestHandler<Command<TUpdateViewModel, TKey>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TUpdateViewModel : class
    {
        public async Task<ServiceResponse> Handle(Command<TUpdateViewModel, TKey> request,
            CancellationToken cancellationToken)
        {
            var existingEntity = await queries.GetByIdAsync(request.Id,
                cancellationToken);

            if (existingEntity == null)
            {
                return ServiceResponse.NotFoundResponse($"{typeof(TEntity).Name} with ID {request.Id} not found");
            }

            try
            {
                var entity = mapper.Map(request.Model, existingEntity);
                await repository.UpdateAsync(entity, cancellationToken);
                return ServiceResponse.OkResponse($"{typeof(TEntity).Name} updated", mapper.Map<TViewModel>(entity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalServerErrorResponse(exception.Message);
            }
        }
    }
}