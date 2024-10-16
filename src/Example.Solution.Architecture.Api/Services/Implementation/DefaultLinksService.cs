using Example.Solution.Architecture.Api.Constants;
using Example.Solution.Architecture.Api.Services.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.Api.Services.Implementation;

public class DefaultLinksService(IHttpContextAccessor httpContextAccessor) : ILinksService
{
    public IEnumerable<string> GetLinks(IPagination pagination, int totalRecords)
    {
        Stack<string> links = [];

        var request = httpContextAccessor.HttpContext?.Request;

        if (request is null)
        {
            return links;
        }

        var pageCount = (int)Math.Ceiling(totalRecords / (double)pagination.PageSize);

        links.Push($"<{request.Scheme}://{request.Host}{request.Path}/{{id}}>;rel={LinkRelations.Item}");

        if (pageCount < 2)
        {
            return links;
        }

        var pattern = $"<{request.Scheme}://{request.Host}{request.Path}?{QueryStrings.PageNumber}={{0}}&{QueryStrings.PageSize}={pagination.PageSize}>;rel={{1}}";

        if (pagination.CurrentPage > 1)
        {
            links.Push(string.Format(pattern, 1, LinkRelations.First));
            links.Push(string.Format(pattern, pagination.CurrentPage - 1, LinkRelations.Previous));
        }

        if (pagination.CurrentPage == pageCount)
        {
            return links;
        }

        links.Push(string.Format(pattern, pagination.CurrentPage + 1, LinkRelations.Next));
        links.Push(string.Format(pattern, pageCount, LinkRelations.Last));

        return links;
    }
}