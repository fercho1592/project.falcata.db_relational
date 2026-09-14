# Current Schema Inventory

This inventory reflects the SQL migrations currently embedded in `Falcata.DBRelational.AppConsole`. DbUp applies these scripts in filename order.

## Schemas

| Schema | Created by | Purpose |
|---|---|---|
| `main` | `0000-Init_Common_schema.sql` | Shared/common application data. |

## Tables

| Table | Created by | Columns | Constraints |
|---|---|---|---|
| `main.users` | `0001-Users-table.sql` | `user_id int identity`, `email nvarchar(50) not null` | `PK_MAIN_USERS` on `user_id` |
| `main.notes` | `0003-Note-table.sql` | `note_id int identity`, `note nvarchar(200) not null` | `PK_MAIN_NOTE` on `note_id` |

## Initialization catalog

The following migration files make up the current initialization catalog:

| Version | Migration file | Type | Objects/data initialized |
|---|---|---|---|
| `0000` | `0000-Init_Common_schema.sql` | Schema | Creates the `main` schema. |
| `0001` | `0001-Users-table.sql` | Table | Creates `main.users` and its primary key. |
| `0002` | `0002_Common_Data_InitUsers.sql` | Seed data | Inserts the initial user with email `feral1592@gmail.com`. |
| `0003` | `0003-Note-table.sql` | Table | Creates `main.notes` and its primary key. |

## Current status

- Current highest migration version: `0003`.
- Application/domain folders currently represented: `Common`.
- `PayRestaurant` is reserved in the project structure but has no migration objects yet.
- New migrations must be added with the next unused four-digit version and embedded as an `EmbeddedResource` in the console project's `.csproj` file.
