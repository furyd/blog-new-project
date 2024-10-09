namespace Example.Solution.Architecture.Api.Features.Customers.Models.Responses;

public class Customer : Requests.Customer
{
    public Guid Id { get; set; }
}