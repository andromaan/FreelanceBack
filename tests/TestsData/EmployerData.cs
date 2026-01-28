using Domain.Models.Employers;

namespace TestsData;

public class EmployerData
{
    public static Employer CreateEmployer(Guid? id = null, Guid? userId = null)
    {
        return new Employer
        {
            Id = id ?? Guid.NewGuid(),
            UserId = userId ?? Guid.NewGuid(),
            CompanyName = "Test Company",
            CompanyWebsite = "https://testcompany.com",
        };
    }
}
