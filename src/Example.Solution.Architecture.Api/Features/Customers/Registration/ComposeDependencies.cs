using Example.Solution.Architecture.Api.Features.Customers.Constants;

namespace Example.Solution.Architecture.Api.Features.Customers.Registration;

public static class ComposeDependencies
{
    public static void AddCustomersFeature(this WebApplication webApplication)
    {
        webApplication
            .MapGet(Routes.List, Controllers.Customers.List)
            .WithName(RouteNames.ListCustomers)
            ;

        webApplication
            .MapGet(Routes.Get, Controllers.Customers.Get)
            .WithName(RouteNames.GetCustomerById)
            ;

        webApplication
            .MapPost(Routes.Create, Controllers.Customers.Create)
            .WithName(RouteNames.CreateCustomer)
            ;

        webApplication
            .MapPut(Routes.Replace, Controllers.Customers.Replace)
            .WithName(RouteNames.ReplaceCustomer)
            ;

        webApplication
            .MapDelete(Routes.Delete, Controllers.Customers.Delete)
            .WithName(RouteNames.DeleteCustomer)
            ;
    }
}
