using DAL.Extensions;
using Domain.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Payments;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.StripePaymentIntentId).HasMaxLength(128);
        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.Status).HasMaxLength(32).IsRequired();

        builder.HasOne(p => p.Contract)
            .WithMany()
            .HasForeignKey(p => p.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ConfigureAudit();
    }
}