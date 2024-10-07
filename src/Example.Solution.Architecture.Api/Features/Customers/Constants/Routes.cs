namespace Example.Solution.Architecture.Api.Features.Customers.Constants;

public static class Routes
{
    public const string Root = "customers";

    public const string List = Root;

    public const string Get = Root + "/{id}";

    public const string Create = Root;

    public const string Replace = Root + "/{id}";

    public const string Delete = Root + "/{id}";
}