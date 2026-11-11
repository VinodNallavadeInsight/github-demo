using Microsoft.AspNetCore.Mvc;

namespace PortPulse.Legacy;

public static class LegacyEndpoints
{
    public static void MapLegacy(this IEndpointRouteBuilder app)
    {
        app.MapGet("/legacy/debug/dwell", (string conn, string code) => {
            var r = new LegacyDwellReport();
            var s = r.Generate(conn, code);
            return Results.Ok(s);
        });
    }
}
