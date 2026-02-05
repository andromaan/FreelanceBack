using BLL.Common.Handlers;
using BLL.Common.Processors;
using BLL.Common.Validators;
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

    /// <summary>
    /// Registers a create validator (legacy approach).
    /// </summary>
    public static IServiceCollection AddCreateValidator<TValidator, TCreateViewModel>(
        this IServiceCollection services)
        where TValidator : class, ICreateValidator<TCreateViewModel>
        where TCreateViewModel : class
    {
        services.AddScoped<ICreateValidator<TCreateViewModel>, TValidator>();
        return services;
    }

    /// <summary>
    /// Registers an update validator (legacy approach).
    /// </summary>
    public static IServiceCollection AddUpdateValidator<TValidator, TEntity, TUpdateViewModel>(
        this IServiceCollection services)
        where TValidator : class, IUpdateValidator<TEntity, TUpdateViewModel>
        where TEntity : class
        where TUpdateViewModel : class
    {
        services.AddScoped<IUpdateValidator<TEntity, TUpdateViewModel>, TValidator>();
        return services;
    }

    /// <summary>
    /// Registers a create processor (legacy approach).
    /// </summary>
    public static IServiceCollection AddCreateProcessor<TProcessor, TEntity, TCreateViewModel>(
        this IServiceCollection services)
        where TProcessor : class, ICreateProcessor<TEntity, TCreateViewModel>
        where TEntity : class
        where TCreateViewModel : class
    {
        services.AddScoped<ICreateProcessor<TEntity, TCreateViewModel>, TProcessor>();
        return services;
    }

    /// <summary>
    /// Registers an update processor (legacy approach).
    /// </summary>
    public static IServiceCollection AddUpdateProcessor<TProcessor, TEntity, TUpdateViewModel>(
        this IServiceCollection services)
        where TProcessor : class, IUpdateProcessor<TEntity, TUpdateViewModel>
        where TEntity : class
        where TUpdateViewModel : class
    {
        services.AddScoped<IUpdateProcessor<TEntity, TUpdateViewModel>, TProcessor>();
        return services;
    }
}
