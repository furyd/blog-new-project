using Dapper;
using Example.Solution.Architecture.Domain.Factories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.Domain.Repositories.Implementation;

public class SqlServerCustomersRepository(IConnectionFactory factory) : ICustomersRepository
{
    public async Task<IReadOnlyCollection<string>> List(IPagination pagination)
    {
        const string sql = """
                           SELECT
                                [Id]
                           ,    [GivenName]
                           ,    [FamilyName]
                           FROM [Customers]
                           ORDER BY [Id]
                           OFFSET @offset ROWS FETCH NEXT @rows ROWS ONLY
                           FOR JSON PATH
                           """;

        var currentPage = pagination.CurrentPage < 1
            ? 0
            : pagination.CurrentPage - 1
            ;

        return await GetJson(sql, new { rows = pagination.PageSize, offset = currentPage * pagination.PageSize });
    }

    public async Task<IReadOnlyCollection<string>> Get(Guid id)
    {
        const string sql = """
                           SELECT
                                [Id]
                           ,    [GivenName]
                           ,    [FamilyName]
                           FROM [Customers]
                           WHERE 1=1
                           AND [Id] = @id
                           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
                           """;

        return await GetJson(sql, new { id });
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

    private async Task<IReadOnlyCollection<string>> GetJson(string query, object? parameters = null)
    {
        await using var connection = await factory.Create();

        return (await connection.QueryAsync<string>(query, parameters)).ToList().AsReadOnly();
    }
}