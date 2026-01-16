using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.FreelancerInfo)
            .WithMany(u => u.Skills)
            .HasForeignKey(p => p.FreelancerInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Skill)
            .WithMany()
            .HasForeignKey(p => p.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}