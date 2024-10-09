using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Settings.Implementation;
using Example.Solution.Architecture.Api.Settings.Registration;
using Example.Solution.Architecture.Domain.Factories.Implementation;
using Example.Solution.Architecture.Domain.Factories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Implementation;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.Api.Features.Customers.Registration;

public static class ComposeDependencies
{
    public static void RegisterCustomersServices(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterSettings(builder.Configuration);
        builder.Services.AddSingleton<IConnectionFactory, SqlServerConnectionFactory<DatabaseSettings>>();
        builder.Services.AddScoped<ICustomersRepository, SqlServerCustomersRepository>();
    }

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
