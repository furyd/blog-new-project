using Example.Solution.Architecture.Api.Features.Customers.Constants;
using Example.Solution.Architecture.Api.Features.Customers.Models.Requests;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Example.Solution.Architecture.Api.Features.Customers.Controllers;

public static class Customers
{
    public static async Task<IResult> List([FromServices] ICustomersRepository repository)
    {
        var customers = await repository.List();

        return customers.Count == 0 ? Results.NoContent() : Results.Ok(customers.Select(Map));
    }

    public static async Task<IResult> Get(
        [FromRoute] Guid id,
        [FromServices] ICustomersRepository repository
        )
    {
        var customer = await repository.Get(id);

        return customer is not null ? Results.Ok(Map(customer)) : Results.NotFound();
    }

    public static async Task<IResult> Create(
        [FromBody] Customer customer,
        [FromServices] ICustomersRepository repository
        )
    {
        var customerId = await repository.Create(customer);

        return Results.CreatedAtRoute(RouteNames.GetCustomerById, new { id = customerId });
    }

    public static async Task<IResult> Replace(
        [FromRoute] Guid id,
        [FromBody] Customer customer,
        [FromServices] ICustomersRepository repository
        )
    {
        await repository.Update(id, customer);

        return Results.Ok();
    }

    public static async Task<IResult> Delete(
        [FromRoute] Guid id,
        [FromServices] ICustomersRepository repository
        )
    {
        await repository.Delete(id);

        return Results.NoContent();
    }

    private static Models.Responses.Customer Map(ICustomer model)
    {
        return new Models.Responses.Customer
        {
            Id = model.Id,
            GivenName = model.GivenName,
            FamilyName = model.FamilyName
        };
    }
}