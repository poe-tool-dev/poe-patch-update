namespace PoeFetchLatestPatch;

public class PatchNumFetcher
{
    public async Task<string> GetPatchNumberAsync()
    {
        using TcpClient client = new TcpClient();
        await client.ConnectAsync("patch.pathofexile.com", 12995);
        using NetworkStream stream = client.GetStream();

        await stream.WriteAsync(new byte[] { 1, 6 });
        byte[] bytes = new byte[1024];
        await stream.ReadAsync(bytes, 0, bytes.Length);
        string str = Encoding.Unicode.GetString(bytes[35..(35 + (bytes[34] * 2))]);

        client.Client.Close(100);

        string patch = str.Split("/", StringSplitOptions.RemoveEmptyEntries).Last();
        return patch;
    }
}
