using Bogus;
using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using Example.Solution.Architecture.Api.IntegrationTests.WebApplicationFactories;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class ListTests
{
    [Fact]
    public async Task Get_ReturnsOK_WhenThereAreCustomers()
    {
        var customers = new Faker<Domain.Repositories.Models.Customer>()
            .RuleFor(model => model.Id, faker => faker.Random.Guid())
            .RuleFor(model => model.GivenName, faker => faker.Name.FirstName())
            .RuleFor(model => model.FamilyName, faker => faker.Name.LastName())
            .Generate(10);

        await using var webApplicationFactory = IntegrationTestingWebApplicationFactory.Create(customers);

        using var client = webApplicationFactory.CreateClient();

        var sut = await client.GetAsync(Routes.Root);

        sut.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await JsonSerializer.DeserializeAsync<List<Customer>>(await sut.Content.ReadAsStreamAsync());

        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Get_ReturnsNoContent_WhenNoCustomers()
    {
        await using var webApplicationFactory = IntegrationTestingWebApplicationFactory.Create();

        using var client = webApplicationFactory.CreateClient();

        var sut = await client.GetAsync(Routes.Root);

        sut.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}