using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class ReplaceTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    private const string NewGivenName = "A2";
    private const string NewFamilyName = "B2";

    [Fact]
    public async Task Replace_ReturnsOk()
    {
        var id = Guid.NewGuid();

        var sut = await Api.Features.Customers.Controllers.Customers.Replace(id, new Api.Features.Customers.Models.Requests.Customer { GivenName = NewGivenName, FamilyName = NewFamilyName }, _repository.Object);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok>();
    }
}
