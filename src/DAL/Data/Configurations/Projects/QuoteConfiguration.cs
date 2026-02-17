using Domain.Models.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Projects;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.ToTable("quotes");
        
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Amount).HasPrecision(18, 2).IsRequired();
        builder.Property(q => q.Message).IsRequired(false);

        builder.HasOne(q => q.Project)
            .WithMany(q => q.Quotes)
            .HasForeignKey(q => q.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(q => q.Freelancer)
            .WithMany()
            .HasForeignKey(q => q.FreelancerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}