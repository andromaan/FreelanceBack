using Domain.Models.Auth.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DAL.Extensions;

namespace DAL.Data.Configurations.Auth.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Email).IsRequired().HasMaxLength(100);

        builder.HasIndex(p => p.Email).IsUnique();

        builder.Property(x => x.PasswordHash).IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ConfigureAudit();
    }
}