using Example.Solution.Architecture.Api.UnitTests.Factories;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class DeleteTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        var id = Guid.NewGuid();

        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.Delete(id, _repository.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
}
