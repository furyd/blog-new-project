using NUnit.Framework;
using System.Net;
using System.Text.Json;
using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;

namespace Example.Solution.Architecture.Api.SpecflowTests.StepDefinitions;

[Binding]
public sealed class CustomersStepDefinitions : IDisposable
{
    private string _url = TestContext.Parameters.Get<string>("webAppUrl", string.Empty);
    private HttpResponseMessage? _response;

    [Given("I am calling the Customers endpoint")]
    public void GivenIAmCallingTheCustomersEndpoint()
    {
        var url = TestContext.Parameters.Get<string>("webAppUrl", string.Empty);
        _url = $"{url}/{Routes.Root}";
    }

    [When("I make a GET request")]
    public async Task WhenIMakeAGetRequest()
    {
        using var client = new HttpClient();
        _response = await client.GetAsync(_url);
    }

    [Then("I should get a (.*) response")]
    public void ThenIShouldGetAResponse(int statusCode)
    {
        _response.Should().NotBeNull();
        _response?.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"a list of customers")]
    public async Task ThenAListOfCustomers()
    {
        var content = await JsonSerializer.DeserializeAsync<List<Customer>>(await _response!.Content.ReadAsStreamAsync());

        content.Should().NotBeNullOrEmpty();
    }


    public void Dispose()
    {
        _response?.Dispose();
    }
}
