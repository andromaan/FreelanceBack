using DAL.Extensions;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class PortfolioItemConfiguration : IEntityTypeConfiguration<PortfolioItem>
{
    public void Configure(EntityTypeBuilder<PortfolioItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(2000);
        builder.Property(p => p.FileUrl).HasMaxLength(512);

        builder.HasOne(p => p.FreelancerInfo)
            .WithMany(u => u.Portfolio)
            .HasForeignKey(p => p.FreelancerInfoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ConfigureAudit();
    }
}