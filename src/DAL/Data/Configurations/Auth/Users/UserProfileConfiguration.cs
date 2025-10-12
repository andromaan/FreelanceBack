using DAL.Extensions;
using Domain.Models.Auth.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Auth.Users;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Bio).HasMaxLength(2000);
        builder.Property(p => p.HourlyRate).HasPrecision(18, 2);
        builder.Property(p => p.Location).HasMaxLength(128);
        builder.Property(p => p.AvatarLogo).HasMaxLength(256);

        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.CountryId).IsRequired(false);
        builder.HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Languages)
            .WithMany();

        builder.ConfigureAudit();
    }
}