# Architecture

Layers: Domain (entities/enums), Data (DbContext/Configurations/Migrations/Seed), Features (vertical slices with services/DTOs/endpoints), Infrastructure/Sql (ADO.NET), Legacy, Components (Blazor). 
Request flow: HTTP -> Endpoint -> Service -> DbContext/SQL -> DTO projection.
