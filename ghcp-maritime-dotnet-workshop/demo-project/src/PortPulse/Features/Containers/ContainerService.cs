using Microsoft.EntityFrameworkCore;
using PortPulse.Data;

namespace PortPulse.Features.Containers;

public interface IContainerService
{
    Task<ContainerDto?> GetByNumberAsync(string number, CancellationToken ct);
    Task<List<ContainerDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}

public class ContainerService : IContainerService
{
    private readonly PortPulseDbContext _db;
    public ContainerService(PortPulseDbContext db) { _db = db; }
    public async Task<ContainerDto?> GetByNumberAsync(string number, CancellationToken ct)
    {
        return await _db.Containers.AsNoTracking()
            .Where(c => c.ContainerNumber == number)
            .Select(c => new ContainerDto(c.Id, c.ContainerNumber, c.Type.ToString(), c.ShippingLineId, c.GateInTime, c.GateOutTime))
            .FirstOrDefaultAsync(ct);
    }
    public async Task<List<ContainerDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        page = page < 1 ? 1 : page; pageSize = pageSize < 1 ? 50 : pageSize;
        return await _db.Containers.AsNoTracking()
            .OrderBy(c => c.ContainerNumber)
            .Skip((page-1)*pageSize).Take(pageSize)
            .Select(c => new ContainerDto(c.Id, c.ContainerNumber, c.Type.ToString(), c.ShippingLineId, c.GateInTime, c.GateOutTime))
            .ToListAsync(ct);
    }
}
