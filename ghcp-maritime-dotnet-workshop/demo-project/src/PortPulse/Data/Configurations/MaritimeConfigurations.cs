using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortPulse.Domain.Entities;

namespace PortPulse.Data.Configurations;

public class TerminalConfiguration : IEntityTypeConfiguration<Terminal>
{
    public void Configure(EntityTypeBuilder<Terminal> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Code).IsRequired().HasMaxLength(10);
        builder.HasIndex(t => t.Code).IsUnique();
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Location).HasMaxLength(150);
    }
}

public class BerthConfiguration : IEntityTypeConfiguration<Berth>
{
    public void Configure(EntityTypeBuilder<Berth> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Code).IsRequired().HasMaxLength(10);
        builder.HasIndex(b => new { b.TerminalId, b.Code }).IsUnique();
        builder.HasOne(b => b.Terminal).WithMany(t => t.Berths).HasForeignKey(b => b.TerminalId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class ShippingLineConfiguration : IEntityTypeConfiguration<ShippingLine>
{
    public void Configure(EntityTypeBuilder<ShippingLine> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Code).IsRequired().HasMaxLength(10);
        builder.HasIndex(s => s.Code).IsUnique();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(150);
    }
}

public class VesselConfiguration : IEntityTypeConfiguration<Vessel>
{
    public void Configure(EntityTypeBuilder<Vessel> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Name).IsRequired().HasMaxLength(150);
        builder.Property(v => v.ImoNumber).IsRequired().HasMaxLength(10);
        builder.HasIndex(v => v.ImoNumber).IsUnique();
        builder.HasOne(v => v.ShippingLine).WithMany(sl => sl.Vessels).HasForeignKey(v => v.ShippingLineId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class BerthBookingConfiguration : IEntityTypeConfiguration<BerthBooking>
{
    public void Configure(EntityTypeBuilder<BerthBooking> builder)
    {
        builder.HasKey(bb => bb.Id);
        builder.Property(bb => bb.RowVersion).IsRowVersion().IsConcurrencyToken();
        builder.HasIndex(bb => new { bb.BerthId, bb.ArrivalTime, bb.DepartureTime });
        builder.HasIndex(bb => bb.ShippingLineId);
        builder.HasIndex(bb => bb.VesselId);
        builder.HasOne(bb => bb.Berth).WithMany(b => b.Bookings).HasForeignKey(bb => bb.BerthId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(bb => bb.Vessel).WithMany(v => v.Bookings).HasForeignKey(bb => bb.VesselId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(bb => bb.ShippingLine).WithMany(sl => sl.Bookings).HasForeignKey(bb => bb.ShippingLineId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class ContainerConfiguration : IEntityTypeConfiguration<Container>
{
    public void Configure(EntityTypeBuilder<Container> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.ContainerNumber).IsRequired().HasMaxLength(11);
        builder.HasIndex(c => c.ContainerNumber).IsUnique();
        builder.HasIndex(c => c.ShippingLineId);
        builder.HasIndex(c => c.BerthBookingId);
        builder.Property(c => c.GateInTime);
        builder.HasQueryFilter(c => !c.IsDeleted);
        builder.HasOne(c => c.BerthBooking).WithMany(bb => bb.Containers).HasForeignKey(c => c.BerthBookingId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.ShippingLine).WithMany().HasForeignKey(c => c.ShippingLineId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class CustomsInspectionConfiguration : IEntityTypeConfiguration<CustomsInspection>
{
    public void Configure(EntityTypeBuilder<CustomsInspection> builder)
    {
        builder.HasKey(ci => ci.Id);
        builder.HasIndex(ci => new { ci.ContainerId, ci.InspectionDate });
        builder.HasOne(ci => ci.Container).WithMany(c => c.Inspections).HasForeignKey(ci => ci.ContainerId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class DemurrageChargeConfiguration : IEntityTypeConfiguration<DemurrageCharge>
{
    public void Configure(EntityTypeBuilder<DemurrageCharge> builder)
    {
        builder.HasKey(dc => dc.Id);
        builder.Property(dc => dc.FreeDays).HasPrecision(10, 2);
        builder.Property(dc => dc.DailyRate).HasPrecision(10, 2);
        builder.Property(dc => dc.TotalCharge).HasPrecision(12, 2);
        builder.HasOne(dc => dc.Container).WithMany().HasForeignKey(dc => dc.ContainerId).OnDelete(DeleteBehavior.Restrict);
    }
}
