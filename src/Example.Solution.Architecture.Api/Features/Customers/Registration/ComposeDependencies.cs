using Example.Solution.Architecture.Api.Features.Customers.Constants;

namespace Example.Solution.Architecture.Api.Features.Customers.Registration;

public static class ComposeDependencies
{
    public static void AddCustomersFeature(this WebApplication webApplication)
    {
        webApplication
            .MapGet(Routes.List, Controllers.Customers.List)
            ;

        webApplication
            .MapGet(Routes.Get, Controllers.Customers.Get)
            ;

        webApplication
            .MapPost(Routes.Create, Controllers.Customers.Create)
            ;

        webApplication
            .MapPut(Routes.Replace, Controllers.Customers.Replace)
            ;

        webApplication
            .MapDelete(Routes.Delete, Controllers.Customers.Delete)
            ;
    }
}
