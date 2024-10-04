using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.HelloWorld;

public class GetTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Get_ReturnsOK()
    {
        using var client = webApplicationFactory.CreateClient();

        var sut = await client.GetAsync(Api.Features.HelloWorld.Constants.Routes.Root);

        sut.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}