using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class GetTests
{
    [Fact]
    public void Get_ReturnsOk_WhenCustomerWithIdExists()
    {
        var id = Guid.NewGuid();

        Customer.Customers =
        [
            new Customer
            {
                Id = id,
                GivenName = "A",
                FamilyName = "B",
            }
        ];

        var sut = Api.Features.Customers.Controllers.Customers.Get(id);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<Customer>>();
    }

    [Fact]
    public void Get_ReturnsNotFound_WhenCustomerWithIdDoesNotExist()
    {
        var id = Guid.NewGuid();

        var sut = Api.Features.Customers.Controllers.Customers.Get(id);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NotFound>();
    }
}
