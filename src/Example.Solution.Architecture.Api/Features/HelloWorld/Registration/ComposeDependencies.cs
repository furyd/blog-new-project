using Example.Solution.Architecture.Api.Features.HelloWorld.Constants;

namespace Example.Solution.Architecture.Api.Features.HelloWorld.Registration;

public static class ComposeDependencies
{
    public static void AddHelloWorldFeature(this WebApplication webApplication)
    {
        webApplication
            .MapGet(Routes.Root, Controllers.HelloWorld.Get)
            ;
    }
}
