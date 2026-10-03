#!/usr/bin/env -S dotnet run
#:include UrlCheck.cs
#:package Spectre.Console@0.57.2

using Spectre.Console;

var urls = new[]
{
    "https://google.com",
    "https://httpstat.us/500",
    "https://github.com"
};

var table = new Table();
table.Border(TableBorder.HeavyHead);

table.AddColumn("URL");
table.AddColumn("Status");
table.AddColumn("Time");
table.AddColumn("Result");

using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };

foreach (var url in urls)
{
    var (status, elapsedMs, isOk) = await UrlCheck.CheckAsync(client, url);

    table.AddRow(
    [
        url,
        status,
        elapsedMs.ToString(),
        isOk ? "[green]✓ OK[/]" : "[red]✗ DOWN[/]"
    ]);
}

AnsiConsole.Write(table);