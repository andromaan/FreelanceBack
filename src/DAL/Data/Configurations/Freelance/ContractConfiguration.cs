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
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.Status).HasMaxLength(32).IsRequired();

        builder.HasOne(p => p.Project)
            .WithMany()
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Employer)
            .WithMany()
            .HasForeignKey(p => p.EmployerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Freelancer)
            .WithMany()
            .HasForeignKey(p => p.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ConfigureAudit();
    }
}