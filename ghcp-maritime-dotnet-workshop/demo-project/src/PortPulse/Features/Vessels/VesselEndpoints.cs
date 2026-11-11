namespace PortPulse.Features.Vessels;

public static class VesselEndpoints
{
    public static void MapVesselEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vessels").WithTags("Vessels");
        group.MapGet("/", async (IVesselService svc, CancellationToken ct) => Results.Ok(await svc.GetAllAsync(ct)));
        group.MapPost("/", async (CreateVesselDto dto, IVesselService svc, CancellationToken ct) => {
            var r = await svc.CreateAsync(dto, ct);
            return Results.Created($"/api/vessels/{r.Id}", r);
        });
    }
}
