using DAL.Extensions;
using Domain.Models.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Messaging;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Content).IsRequired().HasMaxLength(2000);
        builder.Property(p => p.SentAt).IsRequired();

        builder.HasOne(p => p.Contract)
            .WithMany()
            .HasForeignKey(p => p.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Sender)
            .WithMany()
            .HasForeignKey(p => p.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ConfigureAudit();
    }
}