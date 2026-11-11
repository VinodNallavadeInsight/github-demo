# Copilot Instructions
- Use async/await with CancellationToken
- AsNoTracking and DTO projection for reads
- No string-concatenated SQL (parameters only)
- Use TimeProvider for time, UTC for all stored dates
- No secrets or connection strings in logs
- System.Text.Json only
- ProblemDetails for errors
- Structured logging
Do Not: block threads, hardcode strings for SQL, return entities from API, use DateTime.Now/DateTime.UtcNow directly
Architecture: Domain, Data (EF Core), Features (Vertical slices), Infrastructure, Legacy, Components (Blazor Server)
