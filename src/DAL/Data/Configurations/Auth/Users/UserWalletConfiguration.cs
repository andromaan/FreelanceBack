using DAL.Extensions;
using Domain.Models.Auth.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Auth.Users;

public class UserWalletConfiguration : IEntityTypeConfiguration<UserWallet>
{
    public void Configure(EntityTypeBuilder<UserWallet> builder)
    {
        builder.ToTable("user_wallets");
        
        builder.HasKey(uw => uw.Id);

        builder.Property(uw => uw.Balance).HasPrecision(18, 2).IsRequired();
        builder.Property(uw => uw.Currency).HasMaxLength(8).IsRequired();
        
        builder.ConfigureAudit();
    }
}