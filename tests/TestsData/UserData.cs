using Domain.Models.Users;

namespace TestsData;

public class UserData
{
    public static User CreateTestUser(Guid? id = null, string? email = null, string? displayName = null,
        int roleId = 1)
    {
        var userId = id ?? Guid.NewGuid();
        return new User
        {
            Id = userId,
            Email = email ?? $"user_{userId}@test.com",
            DisplayName = displayName ?? $"user_{userId}",
            PasswordHash = string.Empty,
            RoleId = roleId,
            CreatedBy = userId,
        };
    }
}