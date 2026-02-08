using DAL.Extensions;
using Domain.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Contracts;

public class ContractMilestoneConfiguration : IEntityTypeConfiguration<ContractMilestone>
{
    public void Configure(EntityTypeBuilder<ContractMilestone> builder)
    {
        builder.ToTable("contract_milestones");

        builder.HasKey(cm => cm.Id);

        builder.Property(cm => cm.Description).HasMaxLength(2000);
        builder.Property(cm => cm.Amount).HasPrecision(18, 2).IsRequired();
        builder.Property(cm => cm.DueDate).IsRequired();
        builder.Property(cm => cm.Status).HasMaxLength(32)
            .HasConversion(
                v => v.ToString(),
                v => (ContractMilestoneStatus)Enum.Parse(typeof(ContractMilestoneStatus), v)).IsRequired();

        builder.HasOne(cm => cm.Contract)
            .WithMany()
            .HasForeignKey(cm => cm.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ConfigureAudit();
    }
}