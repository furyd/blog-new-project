using Example.Solution.Architecture.Api.Features.Customers.Registration;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.AddCustomersFeature();

app.Run();

namespace Example.Solution.Architecture.Api
{
    public partial class Program;
}