using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;

namespace PortPulse.Features.ShippingLines;

public interface IShippingLineService
{
    Task<List<ShippingLineDto>> GetAllAsync(CancellationToken ct);
    Task<ShippingLineDto> CreateAsync(CreateShippingLineDto dto, CancellationToken ct);
}

public class ShippingLineService : IShippingLineService
{
    private readonly PortPulseDbContext _db;
    public ShippingLineService(PortPulseDbContext db) { _db = db; }
    public async Task<List<ShippingLineDto>> GetAllAsync(CancellationToken ct)
    {
        return await _db.ShippingLines.AsNoTracking()
            .Select(s => new ShippingLineDto(s.Id, s.Code, s.Name))
            .ToListAsync(ct);
    }
    public async Task<ShippingLineDto> CreateAsync(CreateShippingLineDto dto, CancellationToken ct)
    {
        var entity = new ShippingLine { Code = dto.Code, Name = dto.Name };
        _db.ShippingLines.Add(entity);
        await _db.SaveChangesAsync(ct);
        return new ShippingLineDto(entity.Id, entity.Code, entity.Name);
    }
}
