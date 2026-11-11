using Microsoft.EntityFrameworkCore;
using PortPulse.Domain.Entities;

namespace PortPulse.Data;

public class PortPulseDbContext : DbContext
{
    public PortPulseDbContext(DbContextOptions<PortPulseDbContext> options) : base(options)
    {
    }

    public DbSet<Terminal> Terminals => Set<Terminal>();
    public DbSet<Berth> Berths => Set<Berth>();
    public DbSet<ShippingLine> ShippingLines => Set<ShippingLine>();
    public DbSet<Vessel> Vessels => Set<Vessel>();
    public DbSet<BerthBooking> BerthBookings => Set<BerthBooking>();
    public DbSet<Container> Containers => Set<Container>();
    public DbSet<CustomsInspection> CustomsInspections => Set<CustomsInspection>();
    public DbSet<DemurrageCharge> DemurrageCharges => Set<DemurrageCharge>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortPulseDbContext).Assembly);
    }
}
