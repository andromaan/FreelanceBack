using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class Create
{
    public record Command<TViewModel, TEntity, TKey> : IRequest<ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class
    {
        public required TViewModel Model { get; init; }
    }

    public class
        CommandHandler<TViewModel, TEntity, TKey>(IRepository<TEntity, TKey> repository, IMapper mapper)
        : IRequestHandler<Command<TViewModel, TEntity, TKey>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TViewModel : class
    {
        public async Task<ServiceResponse> Handle(Command<TViewModel, TEntity, TKey> request,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<TEntity>(request.Model);
                var createdEntity = await repository.CreateAsync(entity, cancellationToken);
                return ServiceResponse.OkResponse($"{typeof(TEntity).Name} created", createdEntity);
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalServerErrorResponse(exception.Message);
            }
        }
    }
}