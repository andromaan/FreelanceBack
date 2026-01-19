using System.Reflection;
using BLL.Commands;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

public static class CrudRegistrationExtensions
{
    public static void RegisterCrudHandlers<TEntity, TKey>(
        this IServiceCollection services, CrudRegistration<TEntity, TKey> reg)
        where TEntity : Entity<TKey>
    {
        // Create Handler
        RegisterCreateHandler(services, reg);

        // GetAll Handler
        var getAllQuery = typeof(GetAll.Query<>).MakeGenericType(reg.ViewModelType);
        var getAllHandler = typeof(GetAll.QueryHandler<,,>)
            .MakeGenericType(reg.EntityType, reg.KeyType, reg.ViewModelType);
        services.AddTransient(
            typeof(IRequestHandler<,>).MakeGenericType(getAllQuery, typeof(ServiceResponse)),
            getAllHandler);

        // GetById Handler
        var getByIdQuery = typeof(GetById.Query<,>).MakeGenericType(reg.KeyType, reg.ViewModelType);
        var getByIdHandler = typeof(GetById.QueryHandler<,,>)
            .MakeGenericType(reg.EntityType, reg.KeyType, reg.ViewModelType);
        services.AddTransient(
            typeof(IRequestHandler<,>).MakeGenericType(getByIdQuery, typeof(ServiceResponse)),
            getByIdHandler);

        // Update Handler
        RegisterUpdateHandler(services, reg);

        // Delete Handler
        var deleteCommand = typeof(Delete.Command<,>).MakeGenericType(reg.ViewModelType, reg.KeyType);
        var deleteHandler = typeof(Delete.CommandHandler<,,>)
            .MakeGenericType(reg.ViewModelType, reg.EntityType, reg.KeyType);
        services.AddTransient(
            typeof(IRequestHandler<,>).MakeGenericType(deleteCommand, typeof(ServiceResponse)),
            deleteHandler);
    }

    private static void RegisterCreateHandler<TEntity, TKey>(
        IServiceCollection services, CrudRegistration<TEntity, TKey> reg)
        where TEntity : Entity<TKey>
    {
        var commandType = typeof(Create.Command<>).MakeGenericType(reg.CreateViewModelType);
        Type handlerType;

        if (reg is { HasUniqueCheck: true, QueriesInterfaceType: not null })
        {
            handlerType = typeof(Create.CommandHandlerUniqueCheck<,,,,>)
                .MakeGenericType(
                    reg.CreateViewModelType,
                    reg.ViewModelType,
                    reg.EntityType,
                    reg.KeyType,
                    reg.QueriesInterfaceType);
        }
        else
        {
            handlerType = typeof(Create.CommandHandler<,,,>)
                .MakeGenericType(
                    reg.CreateViewModelType,
                    reg.ViewModelType,
                    reg.EntityType,
                    reg.KeyType);
        }

        services.AddTransient(
            typeof(IRequestHandler<,>).MakeGenericType(commandType, typeof(ServiceResponse)),
            handlerType);
    }

    private static void RegisterUpdateHandler<TEntity, TKey>(
        IServiceCollection services, CrudRegistration<TEntity, TKey> reg)
        where TEntity : Entity<TKey>
    {
        var commandType = typeof(Update.Command<,>)
            .MakeGenericType(reg.UpdateViewModelType, reg.KeyType);
        Type handlerType;

        if (reg.HasUniqueCheck && reg.QueriesInterfaceType != null)
        {
            handlerType = typeof(Update.CommandHandlerUniqueCheck<,,,,>)
                .MakeGenericType(
                    reg.UpdateViewModelType,
                    reg.ViewModelType,
                    reg.EntityType,
                    reg.KeyType,
                    reg.QueriesInterfaceType);
        }
        else
        {
            handlerType = typeof(Update.CommandHandler<,,,>)
                .MakeGenericType(
                    reg.UpdateViewModelType,
                    reg.ViewModelType,
                    reg.EntityType,
                    reg.KeyType);
        }

        services.AddTransient(
            typeof(IRequestHandler<,>).MakeGenericType(commandType, typeof(ServiceResponse)),
            handlerType);
    }
}

public class CrudRegistration<TEntity, TKey>
    where TEntity : Entity<TKey>
{
    public Type EntityType => typeof(TEntity);
    public Type KeyType => typeof(TKey);
    
    public required Type ViewModelType { get; init; }
    public required Type CreateViewModelType { get; init; }
    public required Type UpdateViewModelType { get; init; }
    public Type? QueriesInterfaceType { get; init; }
    public bool HasUniqueCheck { get; init; }
}