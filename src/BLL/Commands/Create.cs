using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public partial class Create
{
    public record Command<TCreateViewModel, TEntity, TKey> : IRequest<ServiceResponse>
        where TEntity : Entity<TKey>
        where TCreateViewModel : class
    {
        public required TCreateViewModel Model { get; init; }
    }

    public class
        CommandHandler<TCreateViewModel, TEntity, TKey>(IRepository<TEntity, TKey> repository, IMapper mapper)
        : IRequestHandler<Command<TCreateViewModel, TEntity, TKey>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TCreateViewModel : class
    {
        public async Task<ServiceResponse> Handle(Command<TCreateViewModel, TEntity, TKey> request,
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