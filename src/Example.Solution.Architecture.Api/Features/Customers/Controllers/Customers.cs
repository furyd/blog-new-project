using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;

namespace Example.Solution.Architecture.Api.Features.Customers.Controllers;

public static class Customers
{
    public static IResult Get() => Results.Ok(new List<Customer>{ new() { Id = Guid.NewGuid(), GivenName = "A", FamilyName = "B" }, new() { Id = Guid.NewGuid(), GivenName = "A", FamilyName = "B" } });
}