using Domain.Models.Users;

namespace BLL.ViewModels.UserLanguage;

public class UpdateUserLanguageVM
{
    public int LanguageId { get; set; }
    public ProficiencyLevel ProficiencyLevel { get; set; }
}