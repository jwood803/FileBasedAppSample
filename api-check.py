import requests
from rich.console import Console
from rich.table import Table

urls = [
    "https://google.com",
]

table = Table(title="Uptime Monitor")
table.add_column("URL")
table.add_column("Status")
table.add_column("Time")
table.add_column("Result")

for url in urls:
    try:
        r = requests.get(url, timeout=5)
        ms = r.elapsed.total_seconds() * 1000
        ok = r.status_code == 200
        table.add_row(url, str(r.status_code), f"{ms:.0f} ms",
                       "[green]✓ OK[/]" if ok else "[red]✗ DOWN[/]")
    except requests.RequestException:
        table.add_row(url, "-", "-", "[red]✗ DOWN[/]")

Console().print(table)