using System.Reflection;
using System.Text;
using BLL.Commands.ContractMilestones;
using BLL.Commands.ProjectMilestones;
using BLL.Common.Behaviours;
using BLL.Common.Interfaces.Repositories.Bids;
using BLL.Common.Interfaces.Repositories.Categories;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Interfaces.Repositories.Quotes;
using BLL.Common.Interfaces.Repositories.Skills;
using BLL.Common.Validators;
using BLL.Extensions;
using BLL.Services.ImageService;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.Bid;
using BLL.ViewModels.Category;
using BLL.ViewModels.ContractMilestone;
using BLL.ViewModels.Country;
using BLL.ViewModels.Language;
using BLL.ViewModels.Project;
using BLL.ViewModels.ProjectMilestone;
using BLL.ViewModels.Quote;
using BLL.ViewModels.Skill;
using Domain;
using Domain.Models.Countries;
using Domain.Models.Freelance;
using Domain.Models.Languages;
using Domain.Models.Projects;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BLL;

public static class ConfigureBusinessLogic
{
    public static void AddBusinessLogic(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMediatrConfig();
        services.AddRegistrations();

        services.AddServices();

        services.AddJwtTokenAuth(builder);
        services.AddSwaggerAuth();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Settings.Roles.AnyAuthenticated,
                policy => policy.RequireRole(Settings.Roles.AdminRole, Settings.Roles.EmployerRole,
                    Settings.Roles.FreelancerRole));
            
            options.AddPolicy(Settings.Roles.AdminOrEmployer,
                policy => policy.RequireRole(Settings.Roles.AdminRole, Settings.Roles.EmployerRole));
            
            options.AddPolicy(Settings.Roles.AdminOrFreelancer,
                policy => policy.RequireRole(Settings.Roles.AdminRole, Settings.Roles.FreelancerRole));
        });
    }

    private static void AddMediatrConfig(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }

    private static void AddRegistrations(this IServiceCollection services)
    {
        // registrations for Country
        services.RegisterCrudHandlers(
            new CrudRegistration<Country, int, ICountryQueries>
            {
                ViewModelType = typeof(CountryVM),
                CreateViewModelType = typeof(CreateCountryVM),
                UpdateViewModelType = typeof(UpdateCountryVM)
            });

        // registrations for Language
        services.RegisterCrudHandlers(
            new CrudRegistration<Language, int, ILanguageQueries>
            {
                ViewModelType = typeof(LanguageVM),
                CreateViewModelType = typeof(CreateLanguageVM),
                UpdateViewModelType = typeof(UpdateLanguageVM)
            });

        // registrations for Category
        services.RegisterCrudHandlers(
            new CrudRegistration<Category, int, ICategoryQueries>
            {
                ViewModelType = typeof(CategoryVM),
                CreateViewModelType = typeof(CreateCategoryVM),
                UpdateViewModelType = typeof(UpdateCategoryVM)
            });

        // registrations for Skill
        services.RegisterCrudHandlers(
            new CrudRegistration<Skill, int, ISkillQueries>
            {
                ViewModelType = typeof(SkillVM),
                CreateViewModelType = typeof(CreateSkillVM),
                UpdateViewModelType = typeof(UpdateSkillVM)
            });

        // registrations for Project
        services.RegisterCrudHandlers(
            new CrudRegistration<Project, Guid, IProjectQueries>
            {
                ViewModelType = typeof(ProjectVM),
                CreateViewModelType = typeof(CreateProjectVM),
                UpdateViewModelType = typeof(UpdateProjectVM)
            });

        // registrations for ProjectMilestone
        services.RegisterCrudHandlers(
            new CrudRegistration<ProjectMilestone, Guid, IProjectMilestoneQueries>
            {
                ViewModelType = typeof(ProjectMilestoneVM),
                CreateViewModelType = typeof(CreateProjectMilestoneVM),
                UpdateViewModelType = typeof(UpdateProjectMilestoneVM)
            });
        
        services.AddScoped<IUpdateValidator<ProjectMilestone, UpdateProjectMilestoneVM>, 
            UpdateProjectMilestoneBudgetValidator>();
        
        services.AddScoped<ICreateValidator<CreateProjectMilestoneVM>, 
            CreateProjectMilestoneBudgetValidator>();

        // registrations for ContractMilestone
        services.RegisterCrudHandlers(
            new CrudRegistration<ContractMilestone, Guid, IContractMilestoneQueries>
            {
                ViewModelType = typeof(ContractMilestoneVM),
                CreateViewModelType = typeof(CreateContractMilestoneVM),
                UpdateViewModelType = typeof(UpdateContractMilestoneVM)
            });
        
        services.AddScoped<IUpdateValidator<ContractMilestone, UpdateContractMilestoneVM>, 
            UpdateContractMilestoneBudgetValidator>();
        
        services.AddScoped<ICreateValidator<CreateContractMilestoneVM>, 
            CreateContractMilestoneBudgetValidator>();

        // registrations for Bids
        services.RegisterCrudHandlers(
            new CrudRegistration<Bid, Guid, IBidQueries>
            {
                ViewModelType = typeof(BidVM),
                CreateViewModelType = typeof(CreateBidVM),
                UpdateViewModelType = typeof(UpdateBidVM)
            });

        // registrations for Quotes
        services.RegisterCrudHandlers(
            new CrudRegistration<Quote, Guid, IQuoteQueries>
            {
                ViewModelType = typeof(QuoteVM),
                CreateViewModelType = typeof(CreateQuoteVM),
                UpdateViewModelType = typeof(UpdateQuoteVM)
            });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IImageService, ImageService>();
    }

    private static void AddJwtTokenAuth(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // options.TokenValidationParameters = new TokenValidationParameters
                // {
                //     RequireExpirationTime = true,
                //     ValidateLifetime = true,
                //     ClockSkew = TimeSpan.Zero,
                //     ValidateIssuer = true,
                //     ValidateAudience = true,
                //     ValidateIssuerSigningKey = true,
                //     IssuerSigningKey =
                //         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder!.Configuration["AuthSettings:key"]!)),
                //     ValidIssuer = builder.Configuration["AuthSettings:issuer"],
                //     ValidAudience = builder.Configuration["AuthSettings:audience"]
                // };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:key"]!)),
                    ValidIssuer = builder.Configuration["AuthSettings:issuer"],
                    ValidAudience = builder.Configuration["AuthSettings:audience"]
                };
            });
    }

    private static void AddSwaggerAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "softstream", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введіть JWT токен"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}