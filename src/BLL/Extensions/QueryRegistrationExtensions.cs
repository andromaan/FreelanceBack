using BLL.CommandsQueries.GenericCRUD.GetAll;
using BLL.CommandsQueries.GenericCRUD.GetById;
using BLL.Common.Interfaces.Repositories;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

public static class QueryRegistrationExtensions
{
    public static IServiceCollection AddQueriesHandlers<TEntity, TKey, TViewModel, TQueries>(
        this IServiceCollection services)
        where TEntity : Entity<TKey>
        where TViewModel : class
        where TQueries : class, IQueries<TEntity, TKey>
    {
        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<TViewModel>, ServiceResponse>),
            typeof(GetAll.QueryHandler<TEntity, TKey, TViewModel, TQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<TKey, TViewModel>, ServiceResponse>),
            typeof(GetById.QueryHandler<TEntity, TKey, TViewModel, TQueries>)
        );
        
        return services;
    }
}