using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Models;
using FluentAssertions;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class ListTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task List_ReturnsOk_WhenCustomersExist()
    {
        _repository.Setup(repository => repository.List()).ReturnsAsync([new Customer()]);

        var sut = await Api.Features.Customers.Controllers.Customers.List(_repository.Object);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<IEnumerable<Api.Features.Customers.Models.Responses.Customer>>>();
    }

    [Fact]
    public async Task List_ReturnsNoContent_WhenNoCustomersExist()
    {
        _repository.Setup(repository => repository.List()).ReturnsAsync([]);

        var sut = await Api.Features.Customers.Controllers.Customers.List(_repository.Object);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NoContent>();
    }
}
