#:package Spectre.Console@0.57.2

using System.Diagnostics;
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

using var client = new HttpClient();
var stopwatch = new Stopwatch();
string status;
bool isOk;

foreach (var url in urls)
{
    stopwatch.Restart();

    try
    {
        var response = await client.GetAsync(url);
        status = response.StatusCode.ToString();
        isOk = response.StatusCode == System.Net.HttpStatusCode.OK;
    }
    catch
    {
        status = "-";
        isOk = false;
    }

    stopwatch.Stop();

    table.AddRow(
    [
        url, 
        status,
        stopwatch.ElapsedMilliseconds.ToString(),
        isOk ? "[green]✓ OK[/]" : "[red]✗ DOWN[/]"
    ]);
}

AnsiConsole.Write(table);