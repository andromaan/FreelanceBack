using Domain.Models.Auth;

namespace TestsData;

public class RoleData
{
    public static Role CreateRole(string name, int id = 0)
    {
        return new Role
        {
            Id = id,
            Name = name
        };
    }
}