using DAL.Converters;
using Domain.Common.Interfaces;
using Domain.Models.Auth.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Extensions;

public static class AuditableConfigurationExtension
{
    public static void ConfigureAudit<T>(this EntityTypeBuilder<T> builder)
        where T : class, IAuditableEntity<Guid>
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ModifiedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
        
        builder.Property(x => x.ModifiedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
    }
}