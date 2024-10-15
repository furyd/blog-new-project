using Dapper;
using Example.Solution.Architecture.Domain.Factories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Models;

namespace Example.Solution.Architecture.Domain.Repositories.Implementation;

public class SqlServerCustomersRepository(IConnectionFactory factory) : ICustomersRepository
{
    public async Task<IReadOnlyCollection<ICustomer>> List()
    {
        const string sql = """
                           SELECT
                                [Id]
                           ,    [GivenName]
                           ,    [FamilyName]
                           FROM [Customers]
                           """;

        await using var connection = await factory.Create();

        return (await connection.QueryAsync<Customer>(sql)).ToList().AsReadOnly();
    }

    public async Task<ICustomer?> Get(Guid id)
    {
        const string sql = """
                           SELECT
                                [Id]
                           ,    [GivenName]
                           ,    [FamilyName]
                           FROM [Customers]
                           WHERE 1=1
                           AND [RowId] = @Id
                           """;

        await using var connection = await factory.Create();

        return await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { id });
    }

    public async Task<Guid> Create(ICustomer model)
    {
        const string sql = """
                           INSERT INTO [Customers]
                           (
                                    [GivenName]
                               ,    [FamilyName]
                           )
                           OUTPUT [INSERTED].[Id]
                           VALUES
                           (
                                    @GivenName
                               ,    @FamilyName
                           )
                           """;

        await using var connection = await factory.Create();

        return await connection.QuerySingleAsync<Guid>(sql, new { model.GivenName, model.FamilyName });
    }

    public async Task Update(Guid id, ICustomer model)
    {
        const string sql = """
                           UPDATE [Customers]
                           SET
                               [GivenName] = @GivenName,
                               [FamilyName] = @FamilyName
                           WHERE 1=1
                           AND [Id] = @Id
                           """;

        await using var connection = await factory.Create();

        await connection.ExecuteAsync(sql, new { id, model.GivenName, model.FamilyName });
    }

    public async Task Delete(Guid id)
    {
        const string sql = """
                           DELETE
                           FROM [Customers]
                           WHERE 1=1
                           AND [Id] = @Id
                           """;

        await using var connection = await factory.Create();

        await connection.ExecuteAsync(sql, new { id });
    }
}