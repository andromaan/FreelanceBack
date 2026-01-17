using System.Reflection;
using System.Text;
using BLL.Commands;
using BLL.Common.Behaviours;
using BLL.Services;
using BLL.Services.ImageService;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using Domain;
using Domain.Models.Countries;
using Domain.Models.Languages;
using Domain.Models.Projects;
using Domain.ViewModels.Category;
using Domain.ViewModels.Country;
using Domain.ViewModels.Language;
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
            typeof(IRequestHandler<Create.Command<CreateCountryVM, Country, int>, ServiceResponse>),
            typeof(Create.CommandHandler<CreateCountryVM, Country, int>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<Country, int, CountryVM>, ServiceResponse>),
            typeof(GetAll.QueryHandler<Country, int, CountryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<Country, int, CountryVM>, ServiceResponse>),
            typeof(GetById.QueryHandler<Country, int, CountryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Update.Command<UpdateCountryVM, Country, int>, ServiceResponse>),
            typeof(Update.CommandHandler<UpdateCountryVM, Country, int>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Delete.Command<Country, int>, ServiceResponse>),
            typeof(Delete.CommandHandler<Country, int>)
        );
        
        // registrations for Language
        services.AddTransient(
            typeof(IRequestHandler<Create.Command<CreateLanguageVM, Language, int>, ServiceResponse>),
            typeof(Create.CommandHandler<CreateLanguageVM, Language, int>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<Language, int, LanguageVM>, ServiceResponse>),
            typeof(GetAll.QueryHandler<Language, int, LanguageVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<Language, int, LanguageVM>, ServiceResponse>),
            typeof(GetById.QueryHandler<Language, int, LanguageVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Update.Command<UpdateLanguageVM, Language, int>, ServiceResponse>),
            typeof(Update.CommandHandler<UpdateLanguageVM, Language, int>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Delete.Command<Language, int>, ServiceResponse>),
            typeof(Delete.CommandHandler<Language, int>)
        );
        
        // registrations for Category
        services.AddTransient(
            typeof(IRequestHandler<Create.Command<CreateCategoryVM, Category, Guid>, ServiceResponse>),
            typeof(Create.CommandHandler<CreateCategoryVM, Category, Guid>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetAll.Query<Category, Guid, CategoryVM>, ServiceResponse>),
            typeof(GetAll.QueryHandler<Category, Guid, CategoryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<GetById.Query<Category, Guid, CategoryVM>, ServiceResponse>),
            typeof(GetById.QueryHandler<Category, Guid, CategoryVM>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Update.Command<UpdateCategoryVM, Category, Guid>, ServiceResponse>),
            typeof(Update.CommandHandler<UpdateCategoryVM, Category, Guid>)
        );

        services.AddTransient(
            typeof(IRequestHandler<Delete.Command<Category, Guid>, ServiceResponse>),
            typeof(Delete.CommandHandler<Category, Guid>)
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