using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class CreateTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task Create_UpdatesDataSource_WhenCreatingCustomer()
    {
        var sut = await Api.Features.Customers.Controllers.Customers.Create(new Api.Features.Customers.Models.Requests.Customer { GivenName = "A", FamilyName = "B" }, _repository.Object);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.CreatedAtRoute>();
    }
}
