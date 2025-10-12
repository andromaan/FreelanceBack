using Domain.Common.Abstractions;

namespace Domain.Models.Languages;

public class Language : Entity<int>
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}