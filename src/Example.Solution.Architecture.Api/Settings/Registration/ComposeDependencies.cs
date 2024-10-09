using Example.Solution.Architecture.Api.Settings.Implementation;

namespace Example.Solution.Architecture.Api.Settings.Registration;

public static class ComposeDependencies
{
    public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
    }
}
