using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;
using PortPulse.Domain.Enums;
using Xunit;

namespace PortPulse.Tests.Data;

public class DbContextTests
{
    private static PortPulseDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<PortPulseDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new PortPulseDbContext(options);
    }

    [Fact]
    public async Task CanAddTerminal()
    {
        using var ctx = CreateContext();
        ctx.Terminals.Add(new Terminal { Code = "T1", Name = "Test", Location = "X" });
        await ctx.SaveChangesAsync();
        Assert.Equal(1, await ctx.Terminals.CountAsync());
    }

    [Fact]
    public async Task ContainerSoftDeleteFilter()
    {
        using var ctx = CreateContext();
        var line = new ShippingLine { Code = "L1", Name = "L" };
        ctx.ShippingLines.Add(line);
        var berth = new Berth { Code = "B1", TerminalId = Guid.NewGuid(), Terminal = new Terminal { Code="T1", Name="T", Location="L" } };
        ctx.Berths.Add(berth);
        var vessel = new Vessel { Name = "V1", ImoNumber = "IMO1234567", ShippingLineId = line.Id, ShippingLine = line };
        ctx.Vessels.Add(vessel);
        var booking = new BerthBooking { BerthId = berth.Id, VesselId = vessel.Id, ShippingLineId = line.Id, ArrivalTime = DateTimeOffset.UtcNow, DepartureTime = DateTimeOffset.UtcNow.AddHours(10) };
        ctx.BerthBookings.Add(booking);
        await ctx.SaveChangesAsync();
        var c = new Container { ContainerNumber = "MSKU1234560", Type = ContainerType.Dry, BerthBookingId = booking.Id, ShippingLineId = line.Id, GateInTime = DateTimeOffset.UtcNow };
        ctx.Containers.Add(c);
        await ctx.SaveChangesAsync();
        c.IsDeleted = true;
        await ctx.SaveChangesAsync();
        Assert.Equal(0, await ctx.Containers.CountAsync());
    }
}
