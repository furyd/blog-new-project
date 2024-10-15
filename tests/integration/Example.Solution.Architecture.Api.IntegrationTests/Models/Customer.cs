using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.Api.IntegrationTests.Models;

public class Customer : ICustomer
{
    public Guid Id { get; set; }

    public string GivenName { get; set; } = string.Empty;

    public string FamilyName { get; set; } = string.Empty;
}