using DAL.Extensions;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.CoverLetter).HasMaxLength(2000);
        builder.Property(p => p.Status).HasMaxLength(32).IsRequired();

        builder.HasOne(p => p.Job)
            .WithMany(j => j.Proposals)
            .HasForeignKey(p => p.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Freelancer)
            .WithMany()
            .HasForeignKey(p => p.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ConfigureAudit();
    }
}