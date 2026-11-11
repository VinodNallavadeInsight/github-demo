using Microsoft.AspNetCore.Http.HttpResults;

namespace PortPulse.Features.Terminals;

public static class TerminalEndpoints
{
    public static void MapTerminalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/terminals").WithTags("Terminals");
        group.MapGet("/", async (ITerminalService svc, CancellationToken ct) => Results.Ok(await svc.GetAllAsync(ct)));
        group.MapGet("/{id:guid}", async (Guid id, ITerminalService svc, CancellationToken ct) => {
            var r = await svc.GetByIdAsync(id, ct);
            return r is null ? Results.NotFound() : Results.Ok(r);
        });
        group.MapPost("/", async (CreateTerminalDto dto, ITerminalService svc, CancellationToken ct) => {
            var r = await svc.CreateAsync(dto, ct);
            return Results.Created($"/api/terminals/{r.Id}", r);
        });
        group.MapPut("/{id:guid}", async (Guid id, UpdateTerminalDto dto, ITerminalService svc, CancellationToken ct) => {
            var r = await svc.UpdateAsync(id, dto, ct);
            return r is null ? Results.NotFound() : Results.Ok(r);
        });
    }
}
