using Bogus;
using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using Example.Solution.Architecture.Api.IntegrationTests.WebApplicationFactories;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class GetTest
{
    [Fact]
    public async Task Get_ReturnsOK_WhenCustomerFound()
    {
        var customers = new Faker<Models.Customer>()
            .RuleFor(model => model.Id, faker => faker.Random.Guid())
            .RuleFor(model => model.GivenName, faker => faker.Name.FirstName())
            .RuleFor(model => model.FamilyName, faker => faker.Name.LastName())
            .Generate(1);

        await using var webApplicationFactory = IntegrationTestingWebApplicationFactory.Create(customers);

        using var client = webApplicationFactory.CreateClient();

        var id = customers.First().Id;

        var sut = await client.GetAsync(Routes.Get.Replace("{id}", id.ToString("D")));

        sut.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);

        if (sut.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        var content = await JsonSerializer.DeserializeAsync<Customer>(await sut.Content.ReadAsStreamAsync());

        content.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerNotFound()
    {
        await using var webApplicationFactory = IntegrationTestingWebApplicationFactory.Create();

        using var client = webApplicationFactory.CreateClient();

        var id = Guid.NewGuid();

        var sut = await client.GetAsync(Routes.Get.Replace("{id}", id.ToString("D")));

        sut.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);

        if (sut.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        var content = await JsonSerializer.DeserializeAsync<Customer>(await sut.Content.ReadAsStreamAsync());

        content.Should().NotBeNull();
    }
}