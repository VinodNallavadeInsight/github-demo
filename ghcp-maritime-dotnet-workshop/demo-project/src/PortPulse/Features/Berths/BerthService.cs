using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;

namespace PortPulse.Features.Berths;

public interface IBerthService
{
    Task<List<BerthDto>> GetAllAsync(CancellationToken ct);
    Task<BerthDto?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<BerthDto> CreateAsync(CreateBerthDto dto, CancellationToken ct);
    Task<BerthDto?> UpdateAsync(Guid id, UpdateBerthDto dto, CancellationToken ct);
}

public class BerthService : IBerthService
{
    private readonly PortPulseDbContext _db;
    public BerthService(PortPulseDbContext db) { _db = db; }

    public async Task<List<BerthDto>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Berths.AsNoTracking()
            .Select(b => new BerthDto(b.Id, b.TerminalId, b.Code, b.LengthMeters, b.MaxDraftMeters))
            .ToListAsync(ct);
    }

    public async Task<BerthDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _db.Berths.AsNoTracking()
            .Where(b => b.Id == id)
            .Select(b => new BerthDto(b.Id, b.TerminalId, b.Code, b.LengthMeters, b.MaxDraftMeters))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<BerthDto> CreateAsync(CreateBerthDto dto, CancellationToken ct)
    {
        var entity = new Berth { TerminalId = dto.TerminalId, Code = dto.Code, LengthMeters = dto.LengthMeters, MaxDraftMeters = dto.MaxDraftMeters };
        _db.Berths.Add(entity);
        await _db.SaveChangesAsync(ct);
        return new BerthDto(entity.Id, entity.TerminalId, entity.Code, entity.LengthMeters, entity.MaxDraftMeters);
    }

    public async Task<BerthDto?> UpdateAsync(Guid id, UpdateBerthDto dto, CancellationToken ct)
    {
        var entity = await _db.Berths.FirstOrDefaultAsync(b => b.Id == id, ct);
        if (entity == null) return null;
        entity.Code = dto.Code;
        entity.LengthMeters = dto.LengthMeters;
        entity.MaxDraftMeters = dto.MaxDraftMeters;
        await _db.SaveChangesAsync(ct);
        return new BerthDto(entity.Id, entity.TerminalId, entity.Code, entity.LengthMeters, entity.MaxDraftMeters);
    }
}
