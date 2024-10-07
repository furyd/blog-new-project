using Example.Solution.Architecture.Api.Features.Customers.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Example.Solution.Architecture.Api.IntegrationTests.Features.Customers;

public class CreateTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Create_ReturnsCreated()
    {
        using var client = webApplicationFactory.CreateClient();

        var sut = await client.PostAsync(Routes.Create, JsonContent.Create(new Api.Features.Customers.Models.Requests.Customer { GivenName = "A", FamilyName = "B" }));

        sut.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}