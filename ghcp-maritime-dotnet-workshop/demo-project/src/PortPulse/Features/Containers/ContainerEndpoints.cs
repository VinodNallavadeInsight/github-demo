namespace PortPulse.Features.Containers;

public static class ContainerEndpoints
{
    public static void MapContainerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/containers").WithTags("Containers");
        group.MapGet("/{number}", async (string number, IContainerService svc, CancellationToken ct) => {
            var r = await svc.GetByNumberAsync(number, ct);
            return r is null ? Results.NotFound() : Results.Ok(r);
        });
        group.MapGet("/", async (IContainerService svc, int page = 1, int pageSize = 50, CancellationToken ct = default) => Results.Ok(await svc.GetPagedAsync(page, pageSize, ct)));
    }
}
