using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace PoeFetchLatestPatch;

public class FetchPatchNumHttp
{
    public FetchPatchNumHttp(IConfiguration config)
    {

    }

    [FunctionName("FetchPatchNumHttp")]
    public static async Task<string> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        return await new PatchNumFetcher().GetPatchNumberAsync();
    }


}

