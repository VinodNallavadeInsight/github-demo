# PortPulse Demo Project

ASP.NET Core 8 Minimal APIs + EF Core + Blazor Server components. Maritime terminal operations demo.

## Quick Start
```bash
# SQL Server (optional)
$env:MSSQL_SA_PASSWORD="YourStrong!Passw0rd"
docker compose up -d

# Build and test
dotnet build
dotnet test

# Run
dotnet run --project src/PortPulse
```

## Tech Stack
- .NET 8, C# 12
- EF Core 8 (SQL Server)
- Minimal APIs, Swagger
- xUnit, Moq
- Blazor Server

## Structure
See solution layout in repo root.

## Migrations
```bash
dotnet ef migrations add <Name> --project src/PortPulse --startup-project src/PortPulse --output-dir Data/Migrations
dotnet ef database update --project src/PortPulse --startup-project src/PortPulse
```

## Demo Markers
See `../docs/demo-marker-index.md`
