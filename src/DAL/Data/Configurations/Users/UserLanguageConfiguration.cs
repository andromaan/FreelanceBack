using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Users;

public class UserLanguageConfiguration : IEntityTypeConfiguration<UserLanguage>
{
    public void Configure(EntityTypeBuilder<UserLanguage> builder)
    {
        builder.ToTable("users_languages");
        
        builder.HasKey(ul => new { ul.UserId, ul.LanguageId });

        builder.HasOne(ul => ul.User)
            .WithMany(u => u.Languages)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ul => ul.Language)
            .WithMany()
            .HasForeignKey(ul => ul.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(cm => cm.ProficiencyLevel).HasMaxLength(32)
            .HasConversion(
                v => v.ToString(),
                v => (ProficiencyLevel)Enum.Parse(typeof(ProficiencyLevel), v)).IsRequired();
    }
}