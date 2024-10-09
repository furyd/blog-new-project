using Example.Solution.Architecture.Domain.Factories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace Example.Solution.Architecture.Domain.Factories.Implementation;

public class SqlServerConnectionFactory<T>(IOptions<T> settings) : IConnectionFactory
where T : class, IConnectionString
{
    public async ValueTask<DbConnection> Create()
    {
        var connection = new SqlConnection(settings.Value.ConnectionString);

        await connection.OpenAsync();

        return connection;
    }
}