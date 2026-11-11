using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;
using PortPulse.Domain.Enums;

namespace PortPulse.Features.Bookings;

public interface IBookingService
{
    Task<List<BookingDto>> GetAllAsync(DateTimeOffset? from, DateTimeOffset? to, Guid? berthId, CancellationToken ct);
    Task<BookingDto?> CreateAsync(CreateBookingDto dto, CancellationToken ct);
    Task<bool> CancelAsync(Guid id, CancellationToken ct);
}

public class BookingService : IBookingService
{
    private readonly PortPulseDbContext _db;
    public BookingService(PortPulseDbContext db) { _db = db; }

    public async Task<List<BookingDto>> GetAllAsync(DateTimeOffset? from, DateTimeOffset? to, Guid? berthId, CancellationToken ct)
    {
        var q = _db.BerthBookings.AsNoTracking().AsQueryable();
        if (from.HasValue) q = q.Where(b => b.ArrivalTime >= from.Value);
        if (to.HasValue) q = q.Where(b => b.DepartureTime <= to.Value);
        if (berthId.HasValue) q = q.Where(b => b.BerthId == berthId.Value);
        return await q.Select(b => new BookingDto(b.Id, b.BerthId, b.VesselId, b.ShippingLineId, b.ArrivalTime, b.DepartureTime, b.Status.ToString())).ToListAsync(ct);
    }

    public async Task<BookingDto?> CreateAsync(CreateBookingDto dto, CancellationToken ct)
    {
        var overlaps = await _db.BerthBookings.AnyAsync(b => b.BerthId == dto.BerthId && !(b.DepartureTime <= dto.ArrivalTime || dto.DepartureTime <= b.ArrivalTime), ct);
        if (overlaps) return null;
        var entity = new BerthBooking { BerthId = dto.BerthId, VesselId = dto.VesselId, ShippingLineId = dto.ShippingLineId, ArrivalTime = dto.ArrivalTime, DepartureTime = dto.DepartureTime, Status = BookingStatus.Confirmed };
        _db.BerthBookings.Add(entity);
        await _db.SaveChangesAsync(ct);
        return new BookingDto(entity.Id, entity.BerthId, entity.VesselId, entity.ShippingLineId, entity.ArrivalTime, entity.DepartureTime, entity.Status.ToString());
    }

    public async Task<bool> CancelAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.BerthBookings.FirstOrDefaultAsync(b => b.Id == id);
        if (entity == null) return false;
        entity.Status = BookingStatus.Cancelled;
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
