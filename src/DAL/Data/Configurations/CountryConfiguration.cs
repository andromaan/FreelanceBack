using Domain.Models.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.Alpha2Code)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);
        
        builder.Property(c => c.Alpha3Code)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);
    }
}
