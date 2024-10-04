using System.Net;
using System.Text.Json;
using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class GetTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Get_ReturnsOK()
    {
        using var client = webApplicationFactory.CreateClient();

        var sut = await client.GetAsync(Routes.Root);

        sut.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await JsonSerializer.DeserializeAsync<List<Customer>>(await sut.Content.ReadAsStreamAsync());

        content.Should().NotBeNullOrEmpty();
    }
}