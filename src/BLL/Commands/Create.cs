using AutoMapper;
using BLL.Common;
using BLL.Common.Handlers;
using BLL.Common.Processors;
using BLL.Common.Validators;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;

namespace BLL.Commands;

public class Create
{
    public record Command<TCreateViewModel> : IRequest<ServiceResponse> where TCreateViewModel : class
    {
        public required TCreateViewModel Model { get; init; }
    }

    public class CommandHandler<TCreateViewModel, TViewModel, TEntity, TKey, TQueries>(
        IRepository<TEntity, TKey> repository,
        IMapper mapper,
        TQueries queries,
        IEnumerable<ICreateValidator<TCreateViewModel>> validators,
        IEnumerable<ICreateProcessor<TEntity, TCreateViewModel>> processors,
        IEnumerable<ICreateHandler<TEntity, TCreateViewModel>> handlers)
        : IRequestHandler<Command<TCreateViewModel>, ServiceResponse>
        where TEntity : Entity<TKey>
        where TCreateViewModel : class
        where TViewModel : class
        where TQueries : IQueries<TEntity, TKey>
    {
        public async Task<ServiceResponse> Handle(Command<TCreateViewModel> request,
            CancellationToken cancellationToken)
        {
            // 1. Execute legacy validators (for backward compatibility)
            foreach (var validator in validators)
            {
                var validationResult = await validator.ValidateAsync(
                    request.Model, 
                    cancellationToken);
                    
                if (validationResult != null && !validationResult.Success)
                {
                    return validationResult;
                }
            }
            
            // 2. Map to entity
            var entity = mapper.Map<TEntity>(request.Model);
            
            // 3. Check uniqueness
            if (queries is IUniqueQuery<TEntity, TKey> uniqueQuery)
            {
                if (!await uniqueQuery.IsUniqueAsync(entity, cancellationToken))
                {
                    return ServiceResponse.BadRequest($"{typeof(TEntity).Name} with the same unique fields already exists");
                }
            }

            // 4. Execute legacy processors (for backward compatibility)
            foreach (var processor in processors)
            {
                entity = await processor.ProcessAsync(
                    entity, 
                    request.Model,
                    cancellationToken);
            }
            
            // 5. Execute new unified handlers (validation + processing in one place)
            foreach (var handler in handlers)
            {
                var result = await handler.HandleAsync(
                    entity, 
                    request.Model,
                    cancellationToken);
                    
                if (result.IsFailure)
                {
                    return result.GetFailure();
                }
                
                entity = result.GetSuccess();
            }
            
            // 6. Save to database
            try
            {
                var createdEntity = await repository.CreateAsync(entity, cancellationToken);
                return ServiceResponse.Ok($"{typeof(TEntity).Name} created",
                    mapper.Map<TViewModel>(createdEntity));
            }
            catch (Exception exception)
            {
                return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message );
            }
        }
    }
}