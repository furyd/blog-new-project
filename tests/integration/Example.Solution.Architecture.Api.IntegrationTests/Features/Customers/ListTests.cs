using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class ListTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Get_ReturnsOKOrNoContent()
    {
        using var client = webApplicationFactory.CreateClient();

        var sut = await client.GetAsync(Routes.Root);

        sut.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NoContent);

        if (sut.StatusCode == HttpStatusCode.NoContent)
        {
            return;
        }

        var content = await JsonSerializer.DeserializeAsync<List<Customer>>(await sut.Content.ReadAsStreamAsync());

        content.Should().NotBeNullOrEmpty();
    }
}