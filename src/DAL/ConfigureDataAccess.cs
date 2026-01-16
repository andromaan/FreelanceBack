using BLL.Common;
using BLL.Common.Interfaces.Repositories;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.FreelancersInfo;
using BLL.Common.Interfaces.Repositories.Jobs;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.Proposals;
using BLL.Common.Interfaces.Repositories.Skills;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Interfaces.Repositories.UserSkills;
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
        services.AddScoped(typeof(IQueries<,>), typeof(Repository<,>));

        services.AddScoped<JobRepository>();
        services.AddScoped<IJobRepository>(provider => provider.GetRequiredService<JobRepository>());
        services.AddScoped<IJobQueries>(provider => provider.GetRequiredService<JobRepository>());

        services.AddScoped<CountryRepository>();
        services.AddScoped<ICountryRepository>(provider => provider.GetRequiredService<CountryRepository>());
        services.AddScoped<ICountryQueries>(provider => provider.GetRequiredService<CountryRepository>());

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IUserQueries>(provider => provider.GetRequiredService<UserRepository>());

        services.AddScoped<FreelancerInfoRepository>();
        services.AddScoped<IFreelancerInfoRepository>(provider => provider.GetRequiredService<FreelancerInfoRepository>());
        services.AddScoped<IFreelancerInfoQueries>(provider => provider.GetRequiredService<FreelancerInfoRepository>());

        services.AddScoped<ProposalRepository>();
        services.AddScoped<IProposalRepository>(provider => provider.GetRequiredService<ProposalRepository>());
        services.AddScoped<IProposalQueries>(provider => provider.GetRequiredService<ProposalRepository>());

        services.AddScoped<ContractRepository>();
        services.AddScoped<IContractRepository>(provider => provider.GetRequiredService<ContractRepository>());
        services.AddScoped<IContractQueries>(provider => provider.GetRequiredService<ContractRepository>());

        services.AddScoped<SkillRepository>();
        services.AddScoped<ISkillRepository>(provider => provider.GetRequiredService<SkillRepository>());
        services.AddScoped<ISkillQueries>(provider => provider.GetRequiredService<SkillRepository>());

        services.AddScoped<UserSkillRepository>();
        services.AddScoped<IUserSkillRepository>(provider => provider.GetRequiredService<UserSkillRepository>());
        services.AddScoped<IUserSkillQueries>(provider => provider.GetRequiredService<UserSkillRepository>());

        services.AddScoped<LanguageRepository>();
        services.AddScoped<ILanguageRepository>(provider => provider.GetRequiredService<LanguageRepository>());
        services.AddScoped<ILanguageQueries>(provider => provider.GetRequiredService<LanguageRepository>());
        
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}