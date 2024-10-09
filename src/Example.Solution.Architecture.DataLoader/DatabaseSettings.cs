using Example.Solution.Architecture.Domain.Factories.Interfaces;

namespace Example.Solution.Architecture.DataLoader;

public class DatabaseSettings : IConnectionString
{
    public string ConnectionString { get; set; } = string.Empty;
}