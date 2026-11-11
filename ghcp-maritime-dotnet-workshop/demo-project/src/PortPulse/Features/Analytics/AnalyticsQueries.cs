#nullable enable
using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;

namespace PortPulse.Features.Analytics;

public class AnalyticsQueries
{
    private readonly PortPulseDbContext _db;
    public AnalyticsQueries(PortPulseDbContext db) { _db = db; }

    public async Task<List<string>> GetRecentAsync(CancellationToken ct)
    {
        var q = from b in _db.BerthBookings.AsNoTracking()
                join v in _db.Vessels.AsNoTracking() on b.VesselId equals v.Id
                select v.Name;
        return await q.Take(5).ToListAsync(ct);
    }

    public async Task<List<Container>> GetWithSqlInterpolatedAsync(CancellationToken ct)
    {
        var code = "MAEU";
        return await _db.Containers.FromSqlInterpolated($"SELECT * FROM Containers WHERE ShippingLineId IN (SELECT Id FROM ShippingLines WHERE Code = {code}) AND IsDeleted = 0").ToListAsync(ct);
    }
}
