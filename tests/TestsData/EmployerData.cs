using Domain.Models.Employers;

namespace TestsData;

public class EmployerData
{
    public static Employer CreateEmployer(Guid? id = null, Guid? createdBy = null)
    {
        return new Employer
        {
            Id = id ?? Guid.NewGuid(),
            CompanyName = "Test Company",
            CompanyWebsite = "https://testcompany.com",
            CreatedBy = createdBy ?? Guid.NewGuid(),
        };
    }
}
