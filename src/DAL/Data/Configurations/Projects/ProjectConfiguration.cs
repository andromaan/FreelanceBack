using DAL.Extensions;
using Domain.Models.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Projects;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(2000);
        builder.Property(p => p.Category).HasMaxLength(100);
        builder.Property(p => p.BudgetMin).HasPrecision(18, 2);
        builder.Property(p => p.BudgetMax).HasPrecision(18, 2);
        builder.Property(p => p.IsHourly).IsRequired();
        builder.Property(p => p.Status).HasMaxLength(32).IsRequired();

        builder.HasOne(p => p.Employer)
            .WithMany()
            .HasForeignKey(p => p.EmployerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ConfigureAudit();
    }
}