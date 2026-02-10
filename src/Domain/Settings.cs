namespace Domain;

public static class Settings
{
    public static class Roles
    {
        public const string AnyAuthenticated = "AnyAuthenticated";
        public const string AdminOrModerator = "AdminOrModerator";
        public const string AdminOrEmployer = "AdminOrEmployer";
        public const string AdminOrFreelancer = "AdminOrFreelancer";
        
        public const string AdminRole = "admin";
        public const string EmployerRole = "employer";
        public const string FreelancerRole = "freelancer";
        public const string ModeratorRole = "moderator";
    
        public static readonly List<string> ListOfRoles = new()
        {
            AdminRole,
            EmployerRole,
            FreelancerRole,
            ModeratorRole
        };

    }
}