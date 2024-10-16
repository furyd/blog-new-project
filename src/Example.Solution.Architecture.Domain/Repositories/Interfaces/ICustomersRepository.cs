using Example.Solution.Architecture.Domain.Abstractions;

namespace Example.Solution.Architecture.Domain.Repositories.Interfaces;

public interface ICustomersRepository
{
    Task<IPagedResults<string>> List(IPagination pagination);

    Task<IReadOnlyCollection<string>> Get(Guid id);

    Task<Guid> Create(ICustomer model);

    Task Update(Guid id, ICustomer model);

    Task Delete(Guid id);
}