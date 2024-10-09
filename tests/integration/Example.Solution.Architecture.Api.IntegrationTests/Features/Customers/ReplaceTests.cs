using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.IntegrationTests.WebApplicationFactories;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class ReplaceTests
{
    [Fact]
    public async Task Replace_ReturnsOK()
    {
        await using var webApplicationFactory = IntegrationTestingWebApplicationFactory.Create();

        using var client = webApplicationFactory.CreateClient();

        var sut = await client.PutAsJsonAsync(Routes.Replace.Replace("{id}", Guid.NewGuid().ToString("D")), new Api.Features.Customers.Models.Requests.Customer { GivenName = "A", FamilyName = "B" });

        sut.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}