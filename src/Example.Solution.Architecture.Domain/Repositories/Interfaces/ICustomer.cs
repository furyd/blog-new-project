namespace Example.Solution.Architecture.Domain.Repositories.Interfaces;

public interface ICustomer : ICreateCustomer
{
    Guid Id { get; }
}