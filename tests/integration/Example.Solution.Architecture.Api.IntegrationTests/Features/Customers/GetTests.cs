using System.Net;
using Example.Solution.Architecture.Api.Features.Customers.Constants;
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
    }
}