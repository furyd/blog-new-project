using Example.Solution.Architecture.Api.Features.HelloWorld.Registration;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.AddHelloWorldFeature();

app.Run();

public partial class Program;