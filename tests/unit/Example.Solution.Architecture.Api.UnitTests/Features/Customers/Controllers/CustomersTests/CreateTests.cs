using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class CreateTests
{
    [Fact]
    public void Create_UpdatesDataSource_WhenCreatingCustomer()
    {
        Customer.Customers = [];

        var sut = Api.Features.Customers.Controllers.Customers.Create(new Api.Features.Customers.Models.Requests.Customer { GivenName = "A", FamilyName = "B" });

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Created>();

        Customer.Customers.Count.Should().Be(1);
    }
}
