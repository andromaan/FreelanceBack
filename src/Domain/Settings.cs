namespace Domain;

public static class Settings
{
    public static class Roles
    {
        public const string AnyAuthenticated = "AnyAuthenticated";
        
        public const string AdminRole = "admin";
    
        public static readonly List<string> ListOfRoles = new()
        {
            AdminRole,
        };
    }
}