using DAL.Extensions;
using Domain.Models.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Employers;

public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        builder.ToTable("employers");
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.CompanyWebsite)
            .HasMaxLength(512);
        
        builder.ConfigureAudit(DeleteBehavior.Cascade);
    }
}