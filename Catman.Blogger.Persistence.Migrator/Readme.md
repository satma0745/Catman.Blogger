# Catman.Blogger Database Migrator

This project is responsible for database migrations.
Currently supported (targeted) database management system: `PostgreSQL 12`.


## Requirements

For the correct operation of the tool, You must:

- [x] Install the target database management system (currently `PostgreSQL 12`).
- [x] Create a database and user for the application.
- [x] Create a `pgcrypto` extension for the database (superuser role required).
- [x] Specify the [connection string](https://www.connectionstrings.com/npgsql/standard/) for the
      application database in the `CATMAN_BLOGGER_CONNECTION_STRING` environment variable.


## How to use

### Register migrations

Migrations are stored in the `Catman.Blogger.Persistence.Migrator.Migrations` folder.
Each **migration must** implement the `IMigration` interface and use the `[Migration]` attribute.

The implementation of the `IMigration.Apply` getter should provide sql code for applying the
migration, and the implementation of `IMigration.Undo` for undoing the migration.

`MigrationAttribute.Index` specifies a unique sequential migration index.
Migrations are ordered according to their index.
`MigrationAttribute.Description` is an optional (but desirable) brief description of the migration.

### How migrate

**NOTE**: *Do not try to change the database schema without creating and applying the migration!*

To apply all migrations, run the following command in the **root folder of the solution**:

```
dotnet run -p Catman.Blogger.Persistence.Migrator
```

To migrate to a specific migration, use the `-m` argument with the index of the desired migration.
Example (migrate up or down to migration with index 1):

```
dotnet run -p Catman.Blogger.Persistence.Migrator -m 1
```
