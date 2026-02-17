using DAL.Extensions;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("portfolio");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(2000);
        builder.Property(p => p.PortfolioUrl).HasMaxLength(512);
        
        builder.HasOne(p => p.Freelancer)
            .WithMany(p => p.Portfolio)
            .HasForeignKey(p => p.FreelancerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ConfigureAudit(DeleteBehavior.Cascade);
    }
}