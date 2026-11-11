namespace PortPulse.Features.ShippingLines;

public static class ShippingLineEndpoints
{
    public static void MapShippingLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/shippinglines").WithTags("ShippingLines");
        group.MapGet("/", async (IShippingLineService svc, CancellationToken ct) => Results.Ok(await svc.GetAllAsync(ct)));
        group.MapPost("/", async (CreateShippingLineDto dto, IShippingLineService svc, CancellationToken ct) => {
            var r = await svc.CreateAsync(dto, ct);
            return Results.Created($"/api/shippinglines/{r.Id}", r);
        });
    }
}
