using System.Data.Common;

namespace Example.Solution.Architecture.Domain.Factories.Interfaces;

public interface IConnectionFactory
{
    ValueTask<DbConnection> Create();
}