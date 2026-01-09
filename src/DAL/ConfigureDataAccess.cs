using BLL.Common;
using BLL.Common.Interfaces.Repositories;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.Jobs;
using DAL.Data;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace DAL;

public static class ConfigureDataAccess
{
    public static void AddDataAccess(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var dataSourceBuild = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Default"));

        dataSourceBuild.EnableDynamicJson();
        var dataSource = dataSourceBuild.Build();

        services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(
                dataSource,
                npgsqlDbContextOptionsBuilder =>
                    npgsqlDbContextOptionsBuilder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
            .UseSnakeCaseNamingConvention()
            .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        services.AddScoped<JobRepository>();
        services.AddScoped<IJobRepository>(provider => provider.GetRequiredService<JobRepository>());
        services.AddScoped<IJobQueries>(provider => provider.GetRequiredService<JobRepository>());
        
        services.AddScoped<CountryRepository>();
        services.AddScoped<ICountryRepository>(provider => provider.GetRequiredService<CountryRepository>());
        services.AddScoped<ICountryQueries>(provider => provider.GetRequiredService<CountryRepository>());

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IUserSkillRepository, UserSkillRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
    }
}