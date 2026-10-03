using System.Diagnostics;

public static class UrlCheck
{
    public static async Task<(string Status, long ElapsedMs, bool IsOk)> CheckAsync(
        HttpClient client, string url)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var response = await client.GetAsync(url);
            stopwatch.Stop();
            return (response.StatusCode.ToString(), stopwatch.ElapsedMilliseconds,
                    response.StatusCode == System.Net.HttpStatusCode.OK);
        }
        catch
        {
            stopwatch.Stop();
            return ("-", stopwatch.ElapsedMilliseconds, false);
        }
    }
}