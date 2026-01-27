using BLL.Common;
using BLL.Common.Interfaces.Repositories;
using BLL.Common.Interfaces.Repositories.Bids;
using BLL.Common.Interfaces.Repositories.Categories;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Interfaces.Repositories.Proposals;
using BLL.Common.Interfaces.Repositories.Quotes;
using BLL.Common.Interfaces.Repositories.Skills;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Common.Interfaces.Repositories.WalletTransactions;
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
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IQueries<,>), typeof(Repository<,>));

        services.AddScoped<ProjectRepository>();
        services.AddScoped<IProjectRepository>(provider => provider.GetRequiredService<ProjectRepository>());
        services.AddScoped<IProjectQueries>(provider => provider.GetRequiredService<ProjectRepository>());

        services.AddScoped<CountryRepository>();
        services.AddScoped<ICountryRepository>(provider => provider.GetRequiredService<CountryRepository>());
        services.AddScoped<ICountryQueries>(provider => provider.GetRequiredService<CountryRepository>());

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IUserQueries>(provider => provider.GetRequiredService<UserRepository>());

        services.AddScoped<FreelancerRepository>();
        services.AddScoped<IFreelancerRepository>(provider => provider.GetRequiredService<FreelancerRepository>());
        services.AddScoped<IFreelancerQueries>(provider => provider.GetRequiredService<FreelancerRepository>());

        services.AddScoped<ProposalRepository>();
        services.AddScoped<IProposalRepository>(provider => provider.GetRequiredService<ProposalRepository>());
        services.AddScoped<IProposalQueries>(provider => provider.GetRequiredService<ProposalRepository>());

        services.AddScoped<ContractRepository>();
        services.AddScoped<IContractRepository>(provider => provider.GetRequiredService<ContractRepository>());
        services.AddScoped<IContractQueries>(provider => provider.GetRequiredService<ContractRepository>());

        services.AddScoped<SkillRepository>();
        services.AddScoped<ISkillRepository>(provider => provider.GetRequiredService<SkillRepository>());
        services.AddScoped<ISkillQueries>(provider => provider.GetRequiredService<SkillRepository>());

        services.AddScoped<LanguageRepository>();
        services.AddScoped<ILanguageRepository>(provider => provider.GetRequiredService<LanguageRepository>());
        services.AddScoped<ILanguageQueries>(provider => provider.GetRequiredService<LanguageRepository>());
        
        services.AddScoped<EmployerRepository>();
        services.AddScoped<IEmployerRepository>(provider => provider.GetRequiredService<EmployerRepository>());
        services.AddScoped<IEmployerQueries>(provider => provider.GetRequiredService<EmployerRepository>());
        
        services.AddScoped<EmployerRepository>();
        services.AddScoped<IEmployerRepository>(provider => provider.GetRequiredService<EmployerRepository>());
        services.AddScoped<IEmployerQueries>(provider => provider.GetRequiredService<EmployerRepository>());
        
        services.AddScoped<CategoryRepository>();
        services.AddScoped<ICategoryRepository>(provider => provider.GetRequiredService<CategoryRepository>());
        services.AddScoped<ICategoryQueries>(provider => provider.GetRequiredService<CategoryRepository>());
        
        services.AddScoped<ProjectMilestoneRepository>();
        services.AddScoped<IProjectMilestoneRepository>(provider => provider.GetRequiredService<ProjectMilestoneRepository>());
        services.AddScoped<IProjectMilestoneQueries>(provider => provider.GetRequiredService<ProjectMilestoneRepository>());
        
        services.AddScoped<ContractMilestoneRepository>();
        services.AddScoped<IContractMilestoneRepository>(provider => provider.GetRequiredService<ContractMilestoneRepository>());
        services.AddScoped<IContractMilestoneQueries>(provider => provider.GetRequiredService<ContractMilestoneRepository>());
        
        services.AddScoped<BidRepository>();
        services.AddScoped<IBidRepository>(provider => provider.GetRequiredService<BidRepository>());
        services.AddScoped<IBidQueries>(provider => provider.GetRequiredService<BidRepository>());
        
        services.AddScoped<QuoteRepository>();
        services.AddScoped<IQuoteRepository>(provider => provider.GetRequiredService<QuoteRepository>());
        services.AddScoped<IQuoteQueries>(provider => provider.GetRequiredService<QuoteRepository>());
        
        services.AddScoped<UserWalletRepository>();
        services.AddScoped<IUserWalletRepository>(provider => provider.GetRequiredService<UserWalletRepository>());
        services.AddScoped<IUserWalletQueries>(provider => provider.GetRequiredService<UserWalletRepository>());
        
        services.AddScoped<WalletTransactionRepository>();
        services.AddScoped<IWalletTransactionRepository>(provider => provider.GetRequiredService<WalletTransactionRepository>());
        services.AddScoped<IWalletTransactionQueries>(provider => provider.GetRequiredService<WalletTransactionRepository>());
    }
}