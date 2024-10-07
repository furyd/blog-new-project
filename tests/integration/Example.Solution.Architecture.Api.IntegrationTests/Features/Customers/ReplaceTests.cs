using Example.Solution.Architecture.Api.Features.Customers.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class ReplaceTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Replace_ReturnsOKOrNotFound()
    {
        using var client = webApplicationFactory.CreateClient();

        var sut = await client.PutAsJsonAsync(Routes.Replace.Replace("{id}", Guid.NewGuid().ToString("D")), new Api.Features.Customers.Models.Requests.Customer { GivenName = "A", FamilyName = "B" });

        sut.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }
}