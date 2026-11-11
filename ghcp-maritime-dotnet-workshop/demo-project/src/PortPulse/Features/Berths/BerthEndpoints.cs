namespace PortPulse.Features.Berths;

public static class BerthEndpoints
{
    public static void MapBerthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/berths").WithTags("Berths");
        group.MapGet("/", async (IBerthService svc, CancellationToken ct) => Results.Ok(await svc.GetAllAsync(ct)));
        group.MapGet("/{id:guid}", async (Guid id, IBerthService svc, CancellationToken ct) => {
            var r = await svc.GetByIdAsync(id, ct);
            return r is null ? Results.NotFound() : Results.Ok(r);
        });
        group.MapPost("/", async (CreateBerthDto dto, IBerthService svc, CancellationToken ct) => {
            var r = await svc.CreateAsync(dto, ct);
            return Results.Created($"/api/berths/{r.Id}", r);
        });
        group.MapPut("/{id:guid}", async (Guid id, UpdateBerthDto dto, IBerthService svc, CancellationToken ct) => {
            var r = await svc.UpdateAsync(id, dto, ct);
            return r is null ? Results.NotFound() : Results.Ok(r);
        });
    }
}
