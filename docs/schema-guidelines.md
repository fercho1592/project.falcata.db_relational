# Schema Design & Migration Guidelines

## Naming

- Schemas: one per domain/app (e.g. `main` for common data). Create with `create schema <name>;` as the first script in that domain's folder.
- Tables: lowercase, plural, snake_case (e.g. `users`, `notes`).
- Columns: lowercase snake_case; primary key column named `<table_singular>_id` (e.g. `user_id`), `int identity`.
- Constraints: `PK_<SCHEMA>_<TABLE>` for primary keys (e.g. `PK_MAIN_USERS`); use `FK_<SCHEMA>_<TABLE>_<REF_TABLE>` for foreign keys and `IX_<TABLE>_<COLUMN(S)>` for indexes.

## Indexing

- Every foreign key column should have a supporting non-clustered index unless it's already covered by the primary key.
- Add unique indexes for natural keys enforced at the app level (e.g. `email` on `users`) instead of relying only on application validation.
- Avoid indexing low-cardinality columns unless used in frequent filtered queries.

## Migration Versioning Policy

- Scripts are numbered `NNNN-Description.sql` (schema) or `NNNN_Description.sql` (data seed), 4-digit zero-padded, strictly increasing and unique across the whole folder.
- DbUp applies scripts in filename order and tracks them by name — never edit, delete, or renumber a script once it has been committed/applied; add a new script to change or correct behavior.
- Each new SQL file must be added to the console app's `.csproj` as an `EmbeddedResource`.
- One logical change per script (one table create, one alter, one data seed) to keep upgrades reviewable and reversible in intent.
