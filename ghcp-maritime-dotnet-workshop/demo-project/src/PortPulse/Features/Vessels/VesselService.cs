using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;

namespace PortPulse.Features.Vessels;

public interface IVesselService
{
    Task<List<VesselDto>> GetAllAsync(CancellationToken ct);
    Task<VesselDto> CreateAsync(CreateVesselDto dto, CancellationToken ct);
}

public class VesselService : IVesselService
{
    private readonly PortPulseDbContext _db;
    public VesselService(PortPulseDbContext db) { _db = db; }
    public async Task<List<VesselDto>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Vessels.AsNoTracking()
            .Select(v => new VesselDto(v.Id, v.ShippingLineId, v.Name, v.ImoNumber, v.GrossTonnage))
            .ToListAsync(ct);
    }
    public async Task<VesselDto> CreateAsync(CreateVesselDto dto, CancellationToken ct)
    {
        var entity = new Vessel { ShippingLineId = dto.ShippingLineId, Name = dto.Name, ImoNumber = dto.ImoNumber, GrossTonnage = dto.GrossTonnage };
        _db.Vessels.Add(entity);
        await _db.SaveChangesAsync(ct);
        return new VesselDto(entity.Id, entity.ShippingLineId, entity.Name, entity.ImoNumber, entity.GrossTonnage);
    }
}
