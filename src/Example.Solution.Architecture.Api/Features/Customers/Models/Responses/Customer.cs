namespace Example.Solution.Architecture.Api.Features.Customers.Models.Responses;

public class Customer
{
    public Guid Id { get; set; }

    public string? GivenName { get; set; }

    public string? FamilyName { get; set; }
}