using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.Api.Features.Customers.Models.Requests;

public class Customer : ICustomer
{
    public string GivenName { get; set; } = string.Empty;

    public string FamilyName { get; set; } = string.Empty;
}