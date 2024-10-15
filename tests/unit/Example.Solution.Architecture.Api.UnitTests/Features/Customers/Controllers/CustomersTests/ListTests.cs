using Example.Solution.Architecture.Api.UnitTests.Factories;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class ListTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task List_ReturnsOk_WhenCustomersExist()
    {
        _repository.Setup(repository => repository.List()).ReturnsAsync(["A"]);

        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.List(_repository.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task List_ReturnsNoContent_WhenNoCustomersExist()
    {
        _repository.Setup(repository => repository.List()).ReturnsAsync([]);

        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.List(_repository.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
}
