using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.DataLoader.Models;

public class Customer : ICustomer
{
    public string GivenName { get; set; } = string.Empty;

    public string FamilyName { get; set; } = string.Empty;
}