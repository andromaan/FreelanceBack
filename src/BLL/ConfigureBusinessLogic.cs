using System.Reflection;
using System.Text;
using BLL.Commands;
using BLL.Common.Behaviours;
using BLL.Common.Interfaces.Repositories.Categories;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.Skills;
using BLL.Services;
using BLL.Services.ImageService;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.Category;
using BLL.ViewModels.Country;
using BLL.ViewModels.Language;
using BLL.ViewModels.Skill;
using Domain;
using Domain.Models.Countries;
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
                policy => policy.RequireRole(Settings.Roles.AdminRole));
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
        services.AddTransient(
            typeof(IRequestHandler<Create.Command<CreateCountryVM>, ServiceResponse>),
            typeof(Create.CommandHandlerUniqueCheck<CreateCountryVM, CountryVM, Country, int, ICountryQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<CountryVM>, ServiceResponse>),
            typeof(GetAll.QueryHandler<Country, int, CountryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<int, CountryVM>, ServiceResponse>),
            typeof(GetById.QueryHandler<Country, int, CountryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Update.Command<UpdateCountryVM, int>, ServiceResponse>),
            typeof(Update.CommandHandlerUniqueCheck<UpdateCountryVM, CountryVM, Country, int, ICountryQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Delete.Command<CountryVM, int>, ServiceResponse>),
            typeof(Delete.CommandHandler<CountryVM, Country, int>)
        );
        
        // registrations for Language
        services.AddTransient(
            typeof(IRequestHandler<Create.Command<CreateLanguageVM>, ServiceResponse>),
            typeof(Create.CommandHandlerUniqueCheck<CreateLanguageVM, Language, Language, int, ILanguageQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<LanguageVM>, ServiceResponse>),
            typeof(GetAll.QueryHandler<Language, int, LanguageVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<int, LanguageVM>, ServiceResponse>),
            typeof(GetById.QueryHandler<Language, int, LanguageVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Update.Command<UpdateLanguageVM, int>, ServiceResponse>),
            typeof(Update.CommandHandlerUniqueCheck<UpdateLanguageVM, LanguageVM, Language, int, ILanguageQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Delete.Command<LanguageVM, int>, ServiceResponse>),
            typeof(Delete.CommandHandler<LanguageVM, Language, int>)
        );
        
        // registrations for Category
        services.AddTransient(
            typeof(IRequestHandler<Create.Command<CreateCategoryVM>, ServiceResponse>),
            typeof(Create.CommandHandlerUniqueCheck<CreateCategoryVM, CategoryVM, Category, Guid, ICategoryQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<CategoryVM>, ServiceResponse>),
            typeof(GetAll.QueryHandler<Category, Guid, CategoryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<Guid, CategoryVM>, ServiceResponse>),
            typeof(GetById.QueryHandler<Category, Guid, CategoryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Update.Command<UpdateCategoryVM, Guid>, ServiceResponse>),
            typeof(Update.CommandHandlerUniqueCheck<UpdateCategoryVM, CategoryVM, Category, Guid, ICategoryQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Delete.Command<CategoryVM, Guid>, ServiceResponse>),
            typeof(Delete.CommandHandler<CategoryVM, Category, Guid>)
        );
        
        // registrations for Skill
        services.AddTransient(
            typeof(IRequestHandler<Create.Command<CreateSkillVM>, ServiceResponse>),
            typeof(Create.CommandHandlerUniqueCheck<CreateSkillVM, SkillVM, Skill, int, ISkillQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<SkillVM>, ServiceResponse>),
            typeof(GetAll.QueryHandler<Skill, int, SkillVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<int, SkillVM>, ServiceResponse>),
            typeof(GetById.QueryHandler<Skill, int, SkillVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Update.Command<UpdateSkillVM, int>, ServiceResponse>),
            typeof(Update.CommandHandlerUniqueCheck<UpdateSkillVM, SkillVM, Skill, int, ISkillQueries>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Delete.Command<SkillVM, int>, ServiceResponse>),
            typeof(Delete.CommandHandler<SkillVM, Skill, int>)
        );
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
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder!.Configuration["AuthSettings:key"]!)),
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