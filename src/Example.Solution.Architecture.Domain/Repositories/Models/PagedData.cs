using Example.Solution.Architecture.Domain.Abstractions;

namespace Example.Solution.Architecture.Domain.Repositories.Models;

public record PagedData<T>(int RecordCount, IReadOnlyCollection<T> Data) : IPagedResults<T>;