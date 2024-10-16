namespace Example.Solution.Architecture.Domain.Abstractions;

public interface IPagedResults<out T>
{
    IReadOnlyCollection<T> Data { get; }

    int RecordCount { get; }
}