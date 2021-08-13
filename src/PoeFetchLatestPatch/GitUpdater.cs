using Octokit;

namespace PoeFetchLatestPatch;

public class GitUpdater
{
    private readonly string _pat;

    public GitUpdater(string pat)
    {
        _pat = pat;
    }

    public async Task UpdateToPatch(string patch)
    {
        GitHubClient gh = new(
            new ProductHeaderValue("PoEPatchUpdater"))
        {
            Credentials = new Credentials(_pat)
        };

        (string owner, string reponame, string filepath) = (
            "poe-tool-dev", "latest-patch-version", "latest.txt");

        IReadOnlyList<RepositoryContent> file = await gh.Repository
            .Content.GetAllContents(owner, reponame, filepath);

        string content = file[0].Content.Replace("\n", "");
        if (content != patch)
        {
            var result = await gh.Repository.Content.UpdateFile(
                owner, reponame, filepath, new UpdateFileRequest(
                    $"Updating to {patch}", patch, file[0].Sha));
        }
    }
}
