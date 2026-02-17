using BLL.Common.Interfaces.Repositories;
using BLL.Common.Interfaces.Repositories.Bids;
using BLL.Common.Interfaces.Repositories.Categories;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.ContractPayments;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.DisputeResolutions;
using BLL.Common.Interfaces.Repositories.Disputes;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.Messages;
using BLL.Common.Interfaces.Repositories.Portfolios;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Interfaces.Repositories.Quotes;
using BLL.Common.Interfaces.Repositories.RefreshTokens;
using BLL.Common.Interfaces.Repositories.Reviews;
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
using DAL.Extensions;

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

        services.AddRepository<ProjectRepository, IProjectRepository, IProjectQueries>();
        services.AddRepository<CountryRepository, ICountryRepository, ICountryQueries>();
        services.AddRepository<UserRepository, IUserRepository, IUserQueries>();
        services.AddRepository<FreelancerRepository, IFreelancerRepository, IFreelancerQueries>();
        services.AddRepository<ContractRepository, IContractRepository, IContractQueries>();
        services.AddRepository<SkillRepository, ISkillRepository, ISkillQueries>();
        services.AddRepository<LanguageRepository, ILanguageRepository, ILanguageQueries>();
        services.AddRepository<EmployerRepository, IEmployerRepository, IEmployerQueries>();
        services.AddRepository<CategoryRepository, ICategoryRepository, ICategoryQueries>();
        services.AddRepository<ProjectMilestoneRepository, IProjectMilestoneRepository, IProjectMilestoneQueries>();
        services.AddRepository<ContractMilestoneRepository, IContractMilestoneRepository, IContractMilestoneQueries>();
        services.AddRepository<BidRepository, IBidRepository, IBidQueries>();
        services.AddRepository<QuoteRepository, IQuoteRepository, IQuoteQueries>();
        services.AddRepository<UserWalletRepository, IUserWalletRepository, IUserWalletQueries>();
        services.AddRepository<WalletTransactionRepository, IWalletTransactionRepository, IWalletTransactionQueries>();
        services.AddRepository<MessageRepository, IMessageRepository, IMessageQueries>();
        services.AddRepository<ReviewRepository, IReviewRepository, IReviewQueries>();
        services.AddRepository<ContractPaymentRepository, IContractPaymentRepository, IContractPaymentQueries>();
        services.AddRepository<DisputeRepository, IDisputeRepository, IDisputeQueries>();
        services.AddRepository<DisputeResolutionRepository, IDisputeResolutionRepository, IDisputeResolutionQueries>();
        services.AddRepository<PortfolioRepository, IPortfolioRepository, IPortfolioQueries>();
    }
}