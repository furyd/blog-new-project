using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Responses;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Example.Solution.Architecture.Api.SpecflowTests.StepDefinitions;

[Binding]
public sealed class CustomersStepDefinitions : IDisposable
{
    private readonly string _baseUrl = TestContext.Parameters.Get<string>("webAppUrl", string.Empty);
    private string _endpoint = "";
    private HttpResponseMessage? _response;

    [Given("I am calling the Customers list endpoint")]
    public void GivenIAmCallingTheCustomersEndpoint()
    {
        _endpoint = Routes.List;
    }

    [Given("I am calling the Customers get endpoint with the ID (.*)")]
    public void GivenIAmCallingTheCustomersGetEndpoint(string id)
    {
        _endpoint = Routes.Get.Replace("{id}", id);
    }

    [Given("I am calling the Customers create endpoint")]
    public void GivenIAmCallingTheCustomersCreateEndpoint()
    {
        _endpoint = Routes.Create;
    }

    [Given("I am calling the Customers update endpoint with the ID (.*)")]
    public void GivenIAmCallingTheCustomersUpdateEndpoint(string id)
    {
        _endpoint = Routes.Replace.Replace("{id}", id);
    }

    [Given("I am calling the Customers delete endpoint with the ID (.*)")]
    public void GivenIAmCallingTheCustomersDeleteEndpoint(string id)
    {
        _endpoint = Routes.Delete.Replace("{id}", id);
    }


    [When("I make a GET request")]
    public async Task WhenIMakeAGetRequest()
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, _endpoint);
        await WhenIMakeARequest(message);
    }

    [When("I make a POST request with the following data")]
    public async Task WhenIMakeAPostRequestWithTheFollowingData(Table table)
    {
        using var message = new HttpRequestMessage(HttpMethod.Post, _endpoint);

        foreach (var tableRow in table.Rows)
        {
            var model = new Customer
            {
                GivenName = tableRow["GivenName"],
                FamilyName = tableRow["FamilyName"]
            };

            message.Content = JsonContent.Create(model);
            await WhenIMakeARequest(message);
        }
    }

    [When("I make a PUT request with the following data")]
    public async Task WhenIMakeAPutRequest(Table table)
    {
        using var message = new HttpRequestMessage(HttpMethod.Put, _endpoint);

        foreach (var tableRow in table.Rows)
        {
            var model = new Customer
            {
                GivenName = tableRow["GivenName"],
                FamilyName = tableRow["FamilyName"]
            };

            message.Content = JsonContent.Create(model);
            await WhenIMakeARequest(message);
        }
    }

    [When("I make a DELETE request")]
    public async Task WhenIMakeADeleteRequest()
    {
        using var message = new HttpRequestMessage(HttpMethod.Delete, _endpoint);
        await WhenIMakeARequest(message);
    }

    [Then("I should get a (.*) response")]
    public void ThenIShouldGetAResponse(int statusCode)
    {
        _response.Should().NotBeNull();
        _response?.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then("I should get either a (.*) or (.*) response")]
    public void ThenIShouldGetAResponse(int statusCode, int alternativeStatusCode)
    {
        _response.Should().NotBeNull();
        _response?.StatusCode.Should().BeOneOf((HttpStatusCode)statusCode, (HttpStatusCode)alternativeStatusCode);
    }

    [Then("a list of customers")]
    public async Task ThenAListOfCustomers()
    {
        var content = await JsonSerializer.DeserializeAsync<List<Customer>>(await _response!.Content.ReadAsStreamAsync());

        content.Should().NotBeNullOrEmpty();
    }

    private async Task WhenIMakeARequest(HttpRequestMessage message)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_baseUrl);
        _response = await client.SendAsync(message);
    }

    public void Dispose()
    {
        _response?.Dispose();
    }
}
