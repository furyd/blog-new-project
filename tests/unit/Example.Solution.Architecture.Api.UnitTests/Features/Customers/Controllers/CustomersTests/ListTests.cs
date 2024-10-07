using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class ListTests
{
    [Fact]
    public void List_ReturnsOk_WhenCustomersExist()
    {
        Customer.Customers =
        [
            new Customer
            {
                Id = Guid.NewGuid(),
                GivenName = "A",
                FamilyName = "B",
            }
        ];

        var sut = Api.Features.Customers.Controllers.Customers.List();

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<List<Customer>>>();
    }

    [Fact]
    public void List_ReturnsNoContent_WhenNoCustomersExist()
    {
        Customer.Customers = [];

        var sut = Api.Features.Customers.Controllers.Customers.List();

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NoContent>();
    }
}
