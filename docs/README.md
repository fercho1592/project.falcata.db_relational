# Schema Documentation

This folder is the source of truth for schema specifications, table designs, and domain models for the Falcata ecosystem database. Migrations under `Falcata.DBRelational.AppConsole/script/` must implement what is documented here.

## Contents

- [schema-inventory.md](./schema-inventory.md) — current schemas, tables, and initialization migrations.
- [schema-guidelines.md](./schema-guidelines.md) — schema design standards, naming, indexing, and migration versioning policy.

## Adding domain docs

When adding schema for a new domain (e.g. `BillPlaner`, `PayRestaurant`), add a `docs/<domain>.md` describing the tables, relationships, and business rules for that domain before/alongside writing the migration scripts.
