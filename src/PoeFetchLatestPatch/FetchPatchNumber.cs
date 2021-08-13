using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PoeFetchLatestPatch;

public class FetchPatchNumber
{
    private readonly IConfiguration _configuration;

    public FetchPatchNumber(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [FunctionName("FetchPatchNumber")]
    public async Task Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
    {
        try
        {
            string patch = await new PatchNumFetcher().GetPatchNumberAsync();
            var token = _configuration.GetValue<string>("GitToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                log.LogError("GitToken not provided");
                return;
            }

            var gitUpdater = new GitUpdater(token);
            await gitUpdater.UpdateToPatch(patch);
        }
        catch (Exception ex)
        {
            log.LogError(ex.Message);
        }

    }
}
