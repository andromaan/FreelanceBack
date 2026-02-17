using DAL.Extensions;
using Domain.Models.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Reviews;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rating).HasPrecision(3, 2).IsRequired();
        builder.Property(r => r.ReviewText).HasMaxLength(2000).IsRequired();
        builder.Property(r => r.ReviewerRoleId).HasMaxLength(50).IsRequired();

        builder.HasOne(r => r.Contract)
            .WithMany()
            .HasForeignKey(r => r.ContractId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.ConfigureAudit();
    }
}