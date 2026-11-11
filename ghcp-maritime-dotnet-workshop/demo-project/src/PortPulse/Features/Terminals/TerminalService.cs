using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;

namespace PortPulse.Features.Terminals;

public interface ITerminalService
{
    Task<List<TerminalDto>> GetAllAsync(CancellationToken ct);
    Task<TerminalDto?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<TerminalDto> CreateAsync(CreateTerminalDto dto, CancellationToken ct);
    Task<TerminalDto?> UpdateAsync(Guid id, UpdateTerminalDto dto, CancellationToken ct);
}

public class TerminalService : ITerminalService
{
    private readonly PortPulseDbContext _db;
    private readonly TimeProvider _time;

    public TerminalService(PortPulseDbContext db, TimeProvider time)
    {
        _db = db;
        _time = time;
    }

    public async Task<List<TerminalDto>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Terminals.AsNoTracking()
            .Select(t => new TerminalDto(t.Id, t.Code, t.Name, t.Location))
            .ToListAsync(ct);
    }

    public async Task<TerminalDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _db.Terminals.AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TerminalDto(t.Id, t.Code, t.Name, t.Location))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<TerminalDto> CreateAsync(CreateTerminalDto dto, CancellationToken ct)
    {
        var entity = new Terminal { Code = dto.Code, Name = dto.Name, Location = dto.Location };
        _db.Terminals.Add(entity);
        await _db.SaveChangesAsync(ct);
        return new TerminalDto(entity.Id, entity.Code, entity.Name, entity.Location);
    }

    public async Task<TerminalDto?> UpdateAsync(Guid id, UpdateTerminalDto dto, CancellationToken ct)
    {
        var entity = await _db.Terminals.FirstOrDefaultAsync(t => t.Id == id, ct);
        if (entity == null) return null;
        entity.Code = dto.Code;
        entity.Name = dto.Name;
        entity.Location = dto.Location;
        await _db.SaveChangesAsync(ct);
        return new TerminalDto(entity.Id, entity.Code, entity.Name, entity.Location);
    }
}
