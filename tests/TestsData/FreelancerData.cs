using Domain.Models.Freelance;

namespace TestsData;

public class FreelancerData
{
    public static Freelancer CreateFreelancer(Guid? id = null, Guid? userId = null)
    {
        return new Freelancer
        {
            Id = id ?? Guid.NewGuid(),
            CreatedBy = userId ?? Guid.NewGuid(),
            Bio = "Test Freelancer Bio",
            Location = "Test Location",
        };
    }
}
