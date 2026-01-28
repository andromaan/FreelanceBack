using Domain.Models.Projects;

namespace TestsData;

public class CategoryData
{
    public static Category MainCategory => new()
    {
        Id = 0,
        Name = "MainCategory",
    };
}
