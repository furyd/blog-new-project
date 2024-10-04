using Example.Solution.Architecture.Api.Features.Customers.Constants;

namespace Example.Solution.Architecture.Api.Features.Customers.Registration;

public static class ComposeDependencies
{
    public static void AddCustomersFeature(this WebApplication webApplication)
    {
        webApplication
            .MapGet(Routes.Root, Controllers.Customers.Get)
            ;
    }
}
