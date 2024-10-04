using FluentAssertions;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class GetTests
{
    [Fact]
    public void Get_Returns_Ok()
    {
        var sut = Api.Features.Customers.Controllers.Customers.Get();

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
    }
}
