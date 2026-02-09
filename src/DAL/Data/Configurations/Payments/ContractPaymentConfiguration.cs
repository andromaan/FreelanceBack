using DAL.Converters;
using Domain.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Payments;

public class ContractPaymentConfiguration : IEntityTypeConfiguration<ContractPayment>
{
    public void Configure(EntityTypeBuilder<ContractPayment> builder)
    {
        builder.ToTable("contract_payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PaymentMethod).HasMaxLength(32).IsRequired();
        builder.Property(p => p.PaymentDate)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
        builder.Property(p => p.Amount).HasPrecision(18, 2).IsRequired();

        builder.HasOne(p => p.Contract)
            .WithMany()
            .HasForeignKey(p => p.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Milestone)
            .WithMany()
            .HasForeignKey(p => p.MilestoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}