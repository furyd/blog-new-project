using FluentAssertions;

namespace Example.Solution.Architecture.Api.UnitTests.Features.HelloWorld.Controllers.HelloWorldTests;

public class GetTests
{
    [Fact]
    public void Get_Returns_Ok()
    {
        var sut = Api.Features.HelloWorld.Controllers.HelloWorld.Get();

        sut.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
    }
}
