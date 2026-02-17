using DAL.Extensions;
using Domain.Models.Disputes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Disputes;

public class DisputeResolutionConfiguration : IEntityTypeConfiguration<DisputeResolution>
{
    public void Configure(EntityTypeBuilder<DisputeResolution> builder)
    {
        builder.ToTable("dispute_resolutions");
        
        builder.HasKey(d => d.Id);

        builder.Property(d => d.ResolutionDetails).HasMaxLength(2000).IsRequired();
        
        builder.HasOne<Dispute>()
            .WithOne()
            .HasForeignKey<DisputeResolution>(dr => dr.DisputeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ConfigureAudit();
    }
}
