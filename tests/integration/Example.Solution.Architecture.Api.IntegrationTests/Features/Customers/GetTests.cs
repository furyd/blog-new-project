using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class GetTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Get_ReturnsOKOrNotFound()
    {
        using var client = webApplicationFactory.CreateClient();

        var sut = await client.GetAsync(Routes.Get.Replace("{id}", Guid.NewGuid().ToString("D")));

        sut.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);

        if (sut.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        var content = await JsonSerializer.DeserializeAsync<Customer>(await sut.Content.ReadAsStreamAsync());

        content.Should().NotBeNull();
    }
}