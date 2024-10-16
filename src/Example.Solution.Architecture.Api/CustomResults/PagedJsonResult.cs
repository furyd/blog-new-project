using System.Text;

namespace Example.Solution.Architecture.Api.CustomResults;

public class PagedJsonResult(IEnumerable<string> results, IEnumerable<string> links) : IResult
{
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
        httpContext.Response.Headers.Link = new Microsoft.Extensions.Primitives.StringValues(links.ToArray());

        foreach (var result in results)
        {
            await httpContext.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(result));
        }
    }
}
