using Example.Solution.Architecture.Api.UnitTests.Factories;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class GetTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task Get_ReturnsOk_WhenCustomerWithIdExists()
    {
        var id = Guid.NewGuid();

        _repository.Setup(repository => repository.Get(It.Is<Guid>(value => value == id))).ReturnsAsync(["A"]);

        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.Get(id, _repository.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerWithIdDoesNotExist()
    {
        var id = Guid.NewGuid();

        _repository.Setup(repository => repository.Get(It.Is<Guid>(value => value == id))).ReturnsAsync([]);

        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.Get(id, _repository.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
