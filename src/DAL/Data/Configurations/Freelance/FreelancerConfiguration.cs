using DAL.Extensions;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
{
    public void Configure(EntityTypeBuilder<Freelancer> builder)
    {
        builder.ToTable("freelancers");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Bio).HasMaxLength(2000);
        builder.Property(p => p.HourlyRate).HasPrecision(18, 2);
        builder.Property(p => p.Location).HasMaxLength(128);
        builder.Property(p => p.AvatarLogo).HasMaxLength(256);

        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Freelancer>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.CountryId).IsRequired(false);
        builder.HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Languages)
            .WithMany();
        
        builder.HasMany(p => p.Skills)
            .WithOne(s => s.Freelancer!)
            .HasForeignKey(s => s.FreelancerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ConfigureAudit();
    }
}