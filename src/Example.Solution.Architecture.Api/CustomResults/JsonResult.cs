using System.Text;

namespace Example.Solution.Architecture.Api.CustomResults;

public class JsonResult(IEnumerable<string> results) : IResult
{
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

        foreach (var result in results)
        {
            await httpContext.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(result));
        }
    }
}