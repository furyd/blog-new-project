using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class ReplaceTests
{
    [Fact]
    public void Replace_UpdatesItemInDataSource()
    {
        var id = Guid.NewGuid();

        const string newGivenName = "A2";
        const string newFamilyName = "B2";

        Customer.Customers =
        [
            new Customer
            {
                Id = id,
                GivenName = "A",
                FamilyName = "B",
            }
        ];

        var sut = Api.Features.Customers.Controllers.Customers.Replace(id, new Api.Features.Customers.Models.Requests.Customer { GivenName = newGivenName, FamilyName = newFamilyName });

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok>();

        Customer.Customers.Single(item => item.Id == id).GivenName.Should().Be(newGivenName);
        Customer.Customers.Single(item => item.Id == id).FamilyName.Should().Be(newFamilyName);
    }

    [Fact]
    public void Replace_ReturnsNotFound_WhenCustomerWithIdDoesNotExist()
    {
        var id = Guid.NewGuid();

        var sut = Api.Features.Customers.Controllers.Customers.Replace(id, new Api.Features.Customers.Models.Requests.Customer { GivenName = "A2", FamilyName = "B2" });

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NotFound>();
    }
}
