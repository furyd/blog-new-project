using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class DeleteTests
{
    [Fact]
    public void Delete_RemovesItemFromDataSource()
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

        var sut = Api.Features.Customers.Controllers.Customers.Delete(id);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NoContent>();

        Customer.Customers.Should().BeEmpty();
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenCustomerWithIdDoesNotExist()
    {
        var id = Guid.NewGuid();

        var sut = Api.Features.Customers.Controllers.Customers.Delete(id);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NotFound>();
    }
}
