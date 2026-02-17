using System.Text.Json;
using BLL;
using BLL.Services.PasswordHasher;
using Domain.Models.Auth;
using Domain.Models.Countries;
using Domain.Models.Languages;
using Domain.Models.Projects;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Initializer;

public static partial class DataSeed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
        SeedLanguages(modelBuilder);
        SeedCountries(modelBuilder);
        SeedUsers(modelBuilder);
        SeedSkills(modelBuilder);
    }

    private static void SeedSkills(ModelBuilder modelBuilder)
    {
        var skills = new List<Skill>
        {
            new() { Id = 1, Name = "C#" },
            new() { Id = 2, Name = "Java" },
            new() { Id = 3, Name = "Python" },
            new() { Id = 4, Name = "JavaScript" },
            new() { Id = 5, Name = "SQL" },
            new() { Id = 6, Name = "AWS" },
            new() { Id = 7, Name = "Azure" },
            new() { Id = 8, Name = "Docker" },
            new() { Id = 9, Name = "Kubernetes" },
            new() { Id = 10, Name = "React" }
        };

        modelBuilder.Entity<Skill>().HasData(skills);
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher();

        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var adminUser = new User
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            DisplayName = "Admin",
            PasswordHash = passwordHasher.HashPassword("admin"),
            Email = "admin@mail.com",
            RoleId = Settings.Roles.AdminRole,
            CreatedBy = adminId,
            CreatedAt = DateTime.UtcNow,
            ModifiedBy = adminId,
            ModifiedAt = DateTime.UtcNow
        };

        modelBuilder.Entity<User>().HasData(adminUser);
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        var roles = new List<Role>();

        foreach (var role in Settings.Roles.ListOfRoles)
        {
            roles.Add(new Role { Name = role, Id = role.ToLower() });
        }

        modelBuilder.Entity<Role>().HasData(roles);
    }

    private static void SeedLanguages(ModelBuilder modelBuilder)
    {
        try
        {
            var json = File.ReadAllText("wwwroot/languages/languages.json");
            var languagesDto = JsonSerializer.Deserialize<List<LanguageDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var languages = languagesDto!
                .Select((l, index) => new { Id = index + 1, l.Code, l.Name });

            modelBuilder.Entity<Language>().HasData(languages);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding languages: {ex.Message}");
        }
    }


    private static void SeedCountries(ModelBuilder modelBuilder)
    {
        try
        {
            var json = File.ReadAllText("wwwroot/countries/countries.json");
            var countryDtos = JsonSerializer.Deserialize<List<CountryDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (countryDtos == null || !countryDtos.Any())
            {
                Console.WriteLine("Warning: No countries found in the JSON file or the file is empty.");
                return;
            }

            var countries = countryDtos
                .Where(c => !string.IsNullOrWhiteSpace(c.Alpha2) && !string.IsNullOrWhiteSpace(c.Name) &&
                            !string.IsNullOrWhiteSpace(c.Alpha3))
                .Select((c, index) => new Country
                {
                    Id = index + 1,
                    Name = c.Name.Trim(),
                    Alpha2Code = c.Alpha2.Trim().ToUpper(),
                    Alpha3Code = c.Alpha3.Trim().ToUpper()
                })
                .ToList();

            modelBuilder.Entity<Country>().HasData(countries);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding countries: {ex.Message}");
        }
    }
}