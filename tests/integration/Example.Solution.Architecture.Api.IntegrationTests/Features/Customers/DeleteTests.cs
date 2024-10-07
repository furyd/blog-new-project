using Example.Solution.Architecture.Api.Features.Customers.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class DeleteTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Get_ReturnsNoContentOrNotFound()
    {
        using var client = webApplicationFactory.CreateClient();

        var sut = await client.DeleteAsync(Routes.Delete.Replace("{id}", Guid.NewGuid().ToString("D")));

        sut.StatusCode.Should().BeOneOf(HttpStatusCode.NoContent, HttpStatusCode.NotFound);
    }
}