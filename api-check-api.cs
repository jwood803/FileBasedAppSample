#!/usr/bin/env -S dotnet run
#:include UrlCheck.cs
#:property PublishAot=false
#:sdk Microsoft.NET.Sdk.Web

var urls = new[]
{
    "https://google.com",
    "https://httpstat.us/500",
    "https://github.com"
};

var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };

var app = WebApplication.CreateBuilder(args).Build();

app.MapGet("/health", async () =>
{
    var results = await Task.WhenAll(urls.Select(u => UrlCheck.CheckAsync(client, u)));
    return urls.Zip(results, (url, r) => new
    {
        Url = url,
        r.Status,
        r.ElapsedMs,
        r.IsOk
    });
});

app.Run();
