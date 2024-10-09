using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class DeleteTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        var id = Guid.NewGuid();

        var sut = await Api.Features.Customers.Controllers.Customers.Delete(id, _repository.Object);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NoContent>();
    }
}
