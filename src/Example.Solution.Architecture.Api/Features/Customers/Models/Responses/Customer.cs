namespace Example.Solution.Architecture.Api.Features.Customers.Models.Responses;

public class Customer : Requests.Customer
{
    public static List<Customer> Customers = [];

    public Guid Id { get; set; }
}