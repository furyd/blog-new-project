namespace Example.Solution.Architecture.Domain.Repositories.Interfaces;

public interface IPagination
{
    int PageSize { get; }

    int CurrentPage { get; }
}