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
        CommandHandler<TCreateViewModel, TViewModel, TEntity, TKey>(IRepository<TEntity, TKey> repository, IMapper mapper)
        : IRequestHandler<Command<TCreateViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TCreateViewModel : class
    {
        public async Task<ServiceResponse> Handle(Command<TCreateViewModel> request,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<TEntity>(request.Model);
                var createdEntity = await repository.CreateAsync(entity, cancellationToken);
                return ServiceResponse.OkResponse($"{typeof(TEntity).Name} created",
                    mapper.Map<TViewModel>(createdEntity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalServerErrorResponse(exception.Message);
            }
        }
    }
}