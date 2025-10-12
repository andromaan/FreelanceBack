using Domain.Common.Abstractions;

namespace Domain.Models.Countries;

public class Country : Entity<int>
{
    public string Name { get; set; }
    public string Alpha2Code { get; set; }
    public string Alpha3Code { get; set; }
}