using Example.Solution.Architecture.Api.Features.Customers.Models.Requests;

namespace Example.Solution.Architecture.Api.Features.Customers.Controllers;

public static class Customers
{
    public static IResult List() => Models.Responses.Customer.Customers.Count == 0 ? Results.NoContent() : Results.Ok(Models.Responses.Customer.Customers);

    public static IResult Get(Guid id)
    {
        var customer = Models.Responses.Customer.Customers.FirstOrDefault(item => item.Id == id);

        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    public static IResult Create(Customer customer)
    {
        Models.Responses.Customer.Customers.Add(new Models.Responses.Customer { Id = Guid.NewGuid(), GivenName = customer.GivenName, FamilyName = customer.FamilyName });

        return Results.Created();
    }

    public static IResult Replace(Guid id, Customer customer)
    {
        var existingCustomerIndex = Models.Responses.Customer.Customers.FindIndex(item => item.Id == id);

        if (existingCustomerIndex < 0)
        {
            return Results.NotFound();
        }

        Models.Responses.Customer.Customers[existingCustomerIndex].GivenName = customer.GivenName;
        Models.Responses.Customer.Customers[existingCustomerIndex].FamilyName = customer.FamilyName;

        return Results.Ok();
    }

    public static IResult Delete(Guid id)
    {
        var customer = Models.Responses.Customer.Customers.FirstOrDefault(item => item.Id == id);

        if (customer is null)
        {
            return Results.NotFound();
        }

        Models.Responses.Customer.Customers.Remove(customer);

        return Results.NoContent();
    }
}