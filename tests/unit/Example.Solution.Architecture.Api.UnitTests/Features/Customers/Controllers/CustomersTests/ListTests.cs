using Example.Solution.Architecture.Api.Models;
using Example.Solution.Architecture.Api.Services.Interfaces;
using Example.Solution.Architecture.Api.UnitTests.Factories;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class ListTests
{
    private readonly Mock<ICustomersRepository> _repository = new();
    private readonly Mock<ILinksService> _linksService = new();

    [Fact]
    public async Task List_ReturnsOk_WhenCustomersExist()
    {
        _repository.Setup(repository => repository.List(new Pagination(20, 1))).ReturnsAsync(new PagedData<string>(1, ["A"]));

        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.List(_repository.Object, _linksService.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task List_ReturnsNoContent_WhenNoCustomersExist()
    {
        _repository.Setup(repository => repository.List(new Pagination(20, 1))).ReturnsAsync(new PagedData<string>(0, []));

        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.List(_repository.Object, _linksService.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
}
