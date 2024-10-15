namespace Example.Solution.Architecture.Domain.Repositories.Interfaces;

public interface ICustomersRepository
{
    Task<IReadOnlyCollection<ICustomer>> List();

    Task<ICustomer?> Get(Guid id);

    Task<Guid> Create(ICustomer model);

    Task Update(Guid id, ICustomer model);

    Task Delete(Guid id);
}