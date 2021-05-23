# Migration #1 - Add blogs


### Requirements

The `uuid-ossp` extension is required.
To install extension the superuser should execute the following command:
```postgresql
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
```


### Apply migration

Create `blogs` table:
```postgresql
CREATE TABLE blogs(
    id uuid NOT NULL DEFAULT uuid_generate_v4 () PRIMARY KEY,
    title varchar(100) NOT NULL UNIQUE,
    description varchar(250) NOT NULL,
    creation_date timestamp with time zone NOT NULL DEFAULT current_timestamp
);
```


### Undo migration

Drop `blogs` table:
```postgresql
DROP TABLE blogs;
```
