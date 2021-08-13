using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(PoeFetchLatestPatch.Startup))]
namespace PoeFetchLatestPatch;

public class Startup : FunctionsStartup
{
    public override void ConfigureAppConfiguration(
        IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);
        builder.ConfigurationBuilder
            .AddUserSecrets<Startup>(true)
            .AddEnvironmentVariables();
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddApplicationInsightsTelemetryWorkerService();
    }
}
