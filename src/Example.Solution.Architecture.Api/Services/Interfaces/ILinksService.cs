using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.Api.Services.Interfaces;

public interface ILinksService
{
    IEnumerable<string> GetLinks(IPagination pagination, int totalRecords);
}