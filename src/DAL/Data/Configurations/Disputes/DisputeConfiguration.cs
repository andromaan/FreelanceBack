using DAL.Extensions;
using Domain.Models.Disputes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Disputes;

public class DisputeConfiguration : IEntityTypeConfiguration<Dispute>
{
    public void Configure(EntityTypeBuilder<Dispute> builder)
    {
        builder.ToTable("disputes");
        
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Reason).HasMaxLength(2000).IsRequired();
        builder.Property(d => d.Status).IsRequired();
        builder.Property(d => d.ContractId).IsRequired();
        
        builder.ConfigureAudit();
    }
}
