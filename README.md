# Falcata.DBRelational

Relational database schemas and migrations for the **Falcata ecosystem**, targeting **Microsoft SQL Server**. Migrations are SQL scripts applied with [DbUp](https://dbup.readthedocs.io/) via a small .NET console app.

## Ecosystem

| Repo | Role |
|---|---|
| [project.falcata.bill_planer](https://github.com/fercho1592/project.falcata.bill_planer) | Monthly billing planner / expense tracking Web API |
| [project.falcata.pay_restaurant](https://github.com/fercho1592/project.falcata.pay_restaurant) | Bill-splitting app for groups; calculates user shares and settlements |

Schema/design specs live in this repo and are consumed by the apps above — see [docs/](docs/).

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Docker (for a local SQL Server instance)

## Running migrations locally

1. Start a local SQL Server via Docker Compose (set `DBLOCAL_PASSWORD` in your environment or a `.env` file):

   ```bash
   DBLOCAL_PASSWORD=MyLocalDatabase-Falcata docker compose up -d
   ```

2. Build and run the migrator (creates the database if missing and applies all pending scripts):

   ```bash
   dotnet build
   dotnet run --project Falcata.DBRelational.AppConsole
   ```

   By default it connects to `Server=localhost,1443;Database=falcata-local;User Id=sa;Password=MyLocalDatabase-Falcata;TrustServerCertificate=true`. To target another database, pass a connection string as the first argument:

   ```bash
   dotnet run --project Falcata.DBRelational.AppConsole -- "Server=...;Database=...;User Id=...;Password=...;TrustServerCertificate=true"
   ```

## Adding a new migration

1. Add a new SQL script under `Falcata.DBRelational.AppConsole/script/<Domain>/`, following the naming/versioning rules in [docs/schema-guidelines.md](docs/schema-guidelines.md).
2. Add the file as an `EmbeddedResource` in [Falcata.DBRelational.AppConsole.csproj](Falcata.DBRelational.AppConsole/Falcata.DBRelational.AppConsole.csproj).
3. Run the migrator again to apply it.

## More information

- [AGENTS.md](AGENTS.md) — conventions and context for AI coding agents working in this repo.
- [docs/](docs/) — schema specifications, table designs, and domain models (source of truth for schema).
