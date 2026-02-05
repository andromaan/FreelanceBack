using BLL.Common.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

/// <summary>
/// Extension methods for registering handlers, validators, and processors.
/// </summary>
public static class HandlerRegistrationExtensions
{
    /// <summary>
    /// Registers a create handler for the specified entity and view model types.
    /// </summary>
    public static IServiceCollection AddCreateHandler<THandler, TEntity, TCreateViewModel>(
        this IServiceCollection services)
        where THandler : class, ICreateHandler<TEntity, TCreateViewModel>
        where TEntity : class
        where TCreateViewModel : class
    {
        services.AddScoped<ICreateHandler<TEntity, TCreateViewModel>, THandler>();
        return services;
    }

    /// <summary>
    /// Registers an update handler for the specified entity and view model types.
    /// </summary>
    public static IServiceCollection AddUpdateHandler<THandler, TEntity, TUpdateViewModel>(
        this IServiceCollection services)
        where THandler : class, IUpdateHandler<TEntity, TUpdateViewModel>
        where TEntity : class
        where TUpdateViewModel : class
    {
        services.AddScoped<IUpdateHandler<TEntity, TUpdateViewModel>, THandler>();
        return services;
    }
}
