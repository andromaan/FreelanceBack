using DAL.Converters;
using DAL.Extensions;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("contracts");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.AgreedRate)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(c => c.Status).HasMaxLength(32).IsRequired();
        builder.Property(c => c.StartDate)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
        builder.Property(c => c.EndDate)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");

        builder.HasOne(c => c.Project)
            .WithMany()
            .HasForeignKey(c => c.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Freelancer)
            .WithMany()
            .HasForeignKey(c => c.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ConfigureAudit();
    }
}