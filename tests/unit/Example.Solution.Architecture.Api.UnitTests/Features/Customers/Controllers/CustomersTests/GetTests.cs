using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Models;
using FluentAssertions;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class GetTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task Get_ReturnsOk_WhenCustomerWithIdExists()
    {
        var id = Guid.NewGuid();

        _repository.Setup(repository => repository.Get(It.Is<Guid>(value => value == id))).ReturnsAsync(new Customer());

        var sut = await Api.Features.Customers.Controllers.Customers.Get(id, _repository.Object);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<Api.Features.Customers.Models.Responses.Customer>>();
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerWithIdDoesNotExist()
    {
        var id = Guid.NewGuid();

        _repository.Setup(repository => repository.Get(It.Is<Guid>(value => value == id))).ReturnsAsync((Customer?)null);

        var sut = await Api.Features.Customers.Controllers.Customers.Get(id, _repository.Object);

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.NotFound>();
    }
}
