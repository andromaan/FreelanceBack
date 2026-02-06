using BLL.Commands;
using BLL.Common.Interfaces.Repositories;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

public static class CommandRegistrationExtensions
{
    public static IServiceCollection AddUpdateCommandHandler<TEntity, TKey, TViewModel, TUpdateViewModel, TQueries>(
        this IServiceCollection services)
        where TEntity : Entity<TKey>
        where TViewModel : class
        where TUpdateViewModel : class
        where TQueries : class, IQueries<TEntity, TKey>
        
    {
        services.AddTransient(
            typeof(IRequestHandler<Update.Command<TUpdateViewModel, Guid>, ServiceResponse>),
            typeof(Update.CommandHandler<TUpdateViewModel, TViewModel, TEntity, TKey, TQueries>)
        );
        return services;
    }
    
    public static IServiceCollection AddCreateCommandHandler<TEntity, TKey, TViewModel, TCreateViewModel, TQueries>(
        this IServiceCollection services)
        where TEntity : Entity<TKey>
        where TViewModel : class
        where TCreateViewModel : class
        where TQueries : class, IQueries<TEntity, TKey>
        
    {
        services.AddTransient(
            typeof(IRequestHandler<Create.Command<TCreateViewModel>, ServiceResponse>),
            typeof(Create.CommandHandler<TCreateViewModel, TViewModel, TEntity, TKey, TQueries>)
        );
        return services;
    }
}