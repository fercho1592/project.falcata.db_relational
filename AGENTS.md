# AGENTS.md

## Project Overview

`Falcata.DBRelational` manages relational database schemas and migrations for the **Falcata ecosystem**, targeting **Microsoft SQL Server**. Migrations are plain SQL scripts executed via [DbUp](https://dbup.readthedocs.io/), embedded as resources in a .NET console app.

## Ecosystem

This repo is the schema/migration source of truth for other Falcata services:

| Repo | Role |
|---|---|
| [project.falcata.bill_planer](https://github.com/fercho1592/project.falcata.bill_planer) | Monthly billing planner / expense tracking Web API. Defines schema requirements for plans, categories, budgets, and expenses. |
| [project.falcata.pay_restaurant](https://github.com/fercho1592/project.falcata.pay_restaurant) | Bill-splitting app for groups; calculates user shares and settlements. |

When schema changes are needed to support a feature in either app, the migration should be added here, not in the consuming app's repo.

## Project Structure

```
Falcata.DBRelational.AppConsole/
  Program.cs              # DbUp runner (EnsureDatabase + DeployChanges)
  script/
    Common/                # Shared schema (users, notes, ...)
    PayRestaurant/         # Reserved for pay_restaurant-specific schema (currently empty)
```

- Each app/domain gets its own subfolder under `script/` (e.g. `Common`, `PayRestaurant`; a `BillPlaner` folder should be added when that app's schema is added here).
- SQL files must be added to the `.csproj` as `EmbeddedResource` — DbUp only picks up scripts embedded in the assembly (`WithScriptsEmbeddedInAssembly`).

## Migration File Naming & Versioning

DbUp runs scripts in **filename sort order** and tracks applied scripts by name, so numbering must be sequential and never reused.

Convention observed in `script/Common/`:
- `NNNN-Description.sql` — schema changes, e.g. `0000-Init_Common_schema.sql`, `0001-Users-table.sql`, `0003-Note-table.sql`
- `NNNN_Description.sql` (underscore) — data seed scripts, e.g. `0002_Common_Data_InitUsers.sql`

Rules:
- 4-digit zero-padded, incrementing counter, unique across the whole folder (not per-file-type).
- Never edit or renumber an already-applied/committed migration; add a new one instead.
- Use `create schema <name>;` as the first migration in a new domain folder before creating tables in it.

## Build & Run

```bash
dotnet build
dotnet run --project Falcata.DBRelational.AppConsole
```

The console app defaults to a local connection string (`Server=localhost,1443;Database=falcata-local;...`) if none is passed. Pass a connection string as the first CLI arg to target another environment:

```bash
dotnet run --project Falcata.DBRelational.AppConsole -- "Server=...;Database=...;User Id=...;Password=...;TrustServerCertificate=true"
```

Local SQL Server is provided via `docker-compose.yml` (port `1443`, requires `DBLOCAL_PASSWORD` env var / `.env` file):

```bash
docker compose up -d
```

## Source of Truth

`docs/` is the **primary source of truth** for schema specifications, table designs, and domain models. Before adding or changing a migration, check `docs/` for the intended design; update `docs/` alongside any schema-changing migration.
