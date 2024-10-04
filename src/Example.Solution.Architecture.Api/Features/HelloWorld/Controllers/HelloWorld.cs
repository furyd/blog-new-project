namespace Example.Solution.Architecture.Api.Features.HelloWorld.Controllers;

public static class HelloWorld
{
    public static IResult Get() => Results.Ok("Hello world");
}