# PortPulse AGENTS.md

- Build: `dotnet build`
- Test: `dotnet test`
- Run: `dotnet run --project src/PortPulse`
- Migrations: `dotnet ef migrations add <Name> --project src/PortPulse`
- Do not modify Legacy/ without facilitator approval
- Use async/await with CancellationToken
- Use TimeProvider, AsNoTracking, DTO projection
- No string-concatenated SQL
