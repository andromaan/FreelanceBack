using DAL.Extensions;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(2000);
        builder.Property(p => p.Category).HasMaxLength(100);
        builder.Property(p => p.BudgetMin).HasPrecision(18, 2);
        builder.Property(p => p.BudgetMax).HasPrecision(18, 2);
        builder.Property(p => p.IsHourly).IsRequired();
        builder.Property(p => p.Status).HasMaxLength(32).IsRequired();

        builder.HasOne(p => p.Client)
            .WithMany()
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ConfigureAudit();
    }
}