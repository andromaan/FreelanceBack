using Domain.Common.Abstractions;

namespace Domain.Models.Auth;

public class Role : Entity<int>
{
    public string Name { get; set; } = string.Empty;
}