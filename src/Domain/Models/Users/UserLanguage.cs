using Domain.Models.Languages;

namespace Domain.Models.Users;

public class UserLanguage
{
    public Guid UserId { get; set; }
    public User? User { get; set; } = null!;
    public int LanguageId { get; set; }
    public Language? Language { get; set; } = null!;
    public ProficiencyLevel ProficiencyLevel { get; set; }
}

public enum ProficiencyLevel 
{
    Beginner,
    Elementary,
    Intermediate,
    UpperIntermediate,
    Advanced,
    Proficient
}