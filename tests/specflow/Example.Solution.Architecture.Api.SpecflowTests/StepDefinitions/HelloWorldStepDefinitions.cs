using NUnit.Framework;
using System.Net;

namespace Example.Solution.Architecture.Api.SpecflowTests.StepDefinitions;

[Binding]
public sealed class HelloWorldStepDefinitions : IDisposable
{
    private string _url = TestContext.Parameters.Get<string>("webAppUrl", string.Empty);
    private HttpResponseMessage? _response;

    [Given("I am calling the Hello World endpoint")]
    public void GivenIAmCallingTheHelloWorldEndpoint()
    {
        var url = TestContext.Parameters.Get<string>("webAppUrl", string.Empty);
        _url = $"{url}/{Api.Features.HelloWorld.Constants.Routes.Root}";
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

    public void Dispose()
    {
        _response?.Dispose();
    }
}
