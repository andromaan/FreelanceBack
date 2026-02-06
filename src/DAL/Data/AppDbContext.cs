using System.Reflection;
using DAL.Data.Initializer;
using Domain.Models.Auth;
using Domain.Models.Employers;
using Domain.Models.Freelance;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Freelancer> Freelancers { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
        DataSeed.SeedData(builder);
    }
}