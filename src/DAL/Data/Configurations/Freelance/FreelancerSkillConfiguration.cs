using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.Freelance;

public class FreelancerSkillConfiguration : IEntityTypeConfiguration<FreelancerSkill>
{
    public void Configure(EntityTypeBuilder<FreelancerSkill> builder)
    {
        builder.ToTable("freelancers_skills");
        
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Freelancer)
            .WithMany(u => u.Skills)
            .HasForeignKey(p => p.FreelancerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Skill)
            .WithMany()
            .HasForeignKey(p => p.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}