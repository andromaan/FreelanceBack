using Microsoft.Extensions.DependencyInjection;

namespace DAL.Extensions;

public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddRepository<TRepository, TRepositoryInterface, IQueryInterface>(
        this IServiceCollection services)
        where TRepository : class, TRepositoryInterface, IQueryInterface
        where TRepositoryInterface : class
        where IQueryInterface : class
    {
        services.AddScoped<TRepository>();
        services.AddScoped<TRepositoryInterface>(provider => provider.GetRequiredService<TRepository>());
        services.AddScoped<IQueryInterface>(provider => provider.GetRequiredService<TRepository>());
        
        return services;
    }
}