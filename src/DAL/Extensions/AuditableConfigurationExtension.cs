using DAL.Converters;
using Domain.Common.Interfaces;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Extensions;

public static class AuditableConfigurationExtension
{
    public static void ConfigureAudit<T>(this EntityTypeBuilder<T> builder,
        DeleteBehavior behavior = DeleteBehavior.Restrict)
        where T : class, IAuditableEntity<Guid>
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .IsRequired(false)
            .OnDelete(behavior);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy)
            .IsRequired(false)
            .OnDelete(behavior);

        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");

        builder.Property(x => x.ModifiedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
    }
}