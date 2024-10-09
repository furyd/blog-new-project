namespace Example.Solution.Architecture.Domain.Repositories.Interfaces;

public interface ICustomersRepository
{
    Task<IReadOnlyCollection<ICustomer>> List();

    Task<ICustomer?> Get(Guid id);

    Task<Guid> Create(ICreateCustomer model);

    Task Update(Guid id, ICreateCustomer model);

    Task Delete(Guid id);
}