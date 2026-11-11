namespace PortPulse.Features.Bookings;

public static class BookingEndpoints
{
    public static void MapBookingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bookings").WithTags("Bookings");
        group.MapGet("/", async (IBookingService svc, DateTimeOffset? from, DateTimeOffset? to, Guid? berthId, CancellationToken ct) => Results.Ok(await svc.GetAllAsync(from, to, berthId, ct)));
        group.MapPost("/", async (CreateBookingDto dto, IBookingService svc, CancellationToken ct) => {
            var r = await svc.CreateAsync(dto, ct);
            return r is null ? Results.Conflict() : Results.Created($"/api/bookings/{r.Id}", r);
        });
        group.MapPost("/{id:guid}/cancel", async (Guid id, IBookingService svc, CancellationToken ct) => {
            var ok = await svc.CancelAsync(id, ct);
            return ok ? Results.Ok() : Results.NotFound();
        });
    }
}
