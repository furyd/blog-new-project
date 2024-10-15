using Example.Solution.Architecture.Api.UnitTests.Factories;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Features.Customers.Controllers.CustomersTests;

public class CreateTests
{
    private readonly Mock<ICustomersRepository> _repository = new();

    [Fact]
    public async Task Create_UpdatesDataSource_WhenCreatingCustomer()
    {
        var context = HttpContextFactory.Create();

        var sut = await Api.Features.Customers.Controllers.Customers.Create(new Api.Features.Customers.Models.Requests.Customer { GivenName = "A", FamilyName = "B" }, _repository.Object);

        await sut.ExecuteAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status201Created);
        context.Response.Headers.Location.Should().NotBeEmpty();
    }
}
