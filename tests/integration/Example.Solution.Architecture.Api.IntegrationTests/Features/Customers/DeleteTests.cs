using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.IntegrationTests.WebApplicationFactories;
using FluentAssertions;
using System.Net;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class DeleteTests
{
    [Fact]
    public async Task Get_ReturnsNoContentOrNotFound()
    {
        await using var webApplicationFactory = IntegrationTestingWebApplicationFactory.Create();

        using var client = webApplicationFactory.CreateClient();

        var sut = await client.DeleteAsync(Routes.Delete.Replace("{id}", Guid.NewGuid().ToString("D")));

        sut.StatusCode.Should().BeOneOf(HttpStatusCode.NoContent, HttpStatusCode.NotFound);
    }
}