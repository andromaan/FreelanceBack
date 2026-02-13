using BLL.Commands;
using BLL.Common.Interfaces.Repositories;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

public static class CrudRegistrationExtensions
{
    public static void RegisterCrudHandlers<TEntity, TKey, TQueries>(
        this IServiceCollection services, CrudRegistration<TEntity, TKey, TQueries> reg,
        Type[]? specificCreateVMs = null,
        Type[]? specificUpdateVMs = null)
        where TEntity : Entity<TKey>
        where TQueries : IQueries<TEntity, TKey>
    {
        var handlers = new List<HandlerDescriptor>
        {
            new HandlerDescriptor(
                typeof(GetAll.Query<>),
                typeof(GetAll.QueryHandler<,,,>),
                [reg.ViewModelType],
                [
                    reg.EntityType,
                    reg.KeyType,
                    reg.ViewModelType,
                    reg.QueriesInterfaceType
                ]),

            new HandlerDescriptor(
                typeof(GetById.Query<,>),
                typeof(GetById.QueryHandler<,,,>),
                [reg.KeyType, reg.ViewModelType],
                [
                    reg.EntityType,
                    reg.KeyType,
                    reg.ViewModelType,
                    reg.QueriesInterfaceType
                ]),

            new HandlerDescriptor(
                typeof(Delete.Command<,>),
                typeof(Delete.CommandHandler<,,>),
                [reg.ViewModelType, reg.KeyType],
                [reg.ViewModelType, reg.EntityType, reg.KeyType])
        };

        if (reg.CreateViewModelType != null)
        {
            handlers.Add(new HandlerDescriptor(
                typeof(Create.Command<>),
                typeof(Create.CommandHandler<,,,,>),
                [reg.CreateViewModelType],
                [
                    reg.CreateViewModelType,
                    reg.ViewModelType,
                    reg.EntityType,
                    reg.KeyType,
                    reg.QueriesInterfaceType
                ]));
        }

        if (reg.UpdateViewModelType != null)
        {
            handlers.Add(new HandlerDescriptor(
                typeof(Update.Command<,>),
                typeof(Update.CommandHandler<,,,,>),
                [reg.UpdateViewModelType, reg.KeyType],
                [
                    reg.UpdateViewModelType,
                    reg.ViewModelType,
                    reg.EntityType,
                    reg.KeyType,
                    reg.QueriesInterfaceType
                ]));
        }

        foreach (var handler in handlers)
        {
            RegisterHandler(services, handler);
        }

        // Register additional create handlers
        if (specificCreateVMs != null)
        {
            foreach (var createVm in specificCreateVMs)
            {
                var createHandler = new HandlerDescriptor(
                    typeof(Create.Command<>),
                    typeof(Create.CommandHandler<,,,,>),
                    [createVm],
                    [
                        createVm,
                        reg.ViewModelType,
                        reg.EntityType,
                        reg.KeyType,
                        reg.QueriesInterfaceType
                    ]);
                RegisterHandler(services, createHandler);
            }
        }

        // Register additional update handlers
        if (specificUpdateVMs != null)
        {
            foreach (var updateVm in specificUpdateVMs)
            {
                var updateHandler = new HandlerDescriptor(
                    typeof(Update.Command<,>),
                    typeof(Update.CommandHandler<,,,,>),
                    [updateVm, reg.KeyType],
                    [
                        updateVm,
                        reg.ViewModelType,
                        reg.EntityType,
                        reg.KeyType,
                        reg.QueriesInterfaceType
                    ]);
                RegisterHandler(services, updateHandler);
            }
        }
    }

    private static void RegisterHandler(IServiceCollection services, HandlerDescriptor descriptor)
    {
        var requestType = descriptor.RequestType.MakeGenericType(descriptor.RequestTypeArgs);
        var handlerType = descriptor.HandlerType.MakeGenericType(descriptor.HandlerTypeArgs);
        var serviceType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(ServiceResponse));

        services.AddTransient(serviceType, handlerType);
    }

    private record HandlerDescriptor(
        Type RequestType,
        Type HandlerType,
        Type[] RequestTypeArgs,
        Type[] HandlerTypeArgs);
}

public class CrudRegistration<TEntity, TKey, TQueries>
    where TEntity : Entity<TKey>
    where TQueries : IQueries<TEntity, TKey>
{
    public Type EntityType => typeof(TEntity);
    public Type KeyType => typeof(TKey);
    public Type QueriesInterfaceType => typeof(TQueries);

    public required Type ViewModelType { get; init; }
    public Type? CreateViewModelType { get; init; }
    public Type? UpdateViewModelType { get; init; }
}