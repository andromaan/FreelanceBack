using DAL.Converters;
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
        builder.Property(p => p.Budget).HasPrecision(18, 2);
        builder.Property(p => p.Deadline)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
        
        // TODO: rethink this mb
        // builder.Property(p => p.Status).HasMaxLength(32).IsRequired();
        builder.Property(p => p.Status).HasMaxLength(32)
            .HasConversion(
                v => v.ToString(), 
                v => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), v)).IsRequired();

        builder.HasMany(p => p.Categories)
            .WithMany()
            .UsingEntity(join => join.ToTable("projects_categories"));
        
        builder.ConfigureAudit();
    }
}