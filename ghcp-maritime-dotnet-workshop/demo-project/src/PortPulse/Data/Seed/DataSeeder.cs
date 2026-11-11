using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Domain.Entities;
using PortPulse.Domain.Enums;

namespace PortPulse.Data.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(PortPulseDbContext context, bool isDevelopment)
    {
        if (!isDevelopment) return;
        if (await context.Terminals.AnyAsync()) return;

        var random = new Random(123456789);
        var now = DateTimeOffset.UtcNow;

        // Terminals
        var terminals = new List<Terminal>
        {
            new() { Code = "NGT-A", Name = "Northgate Alpha Terminal", Location = "Rotterdam" },
            new() { Code = "NGT-B", Name = "Northgate Bravo Terminal", Location = "Hamburg" },
            new() { Code = "NGT-C", Name = "Northgate Charlie Terminal", Location = "Antwerp" }
        };
        context.Terminals.AddRange(terminals);
        await context.SaveChangesAsync();

        // Berths
        var berths = new List<Berth>();
        foreach (var t in terminals)
        {
            for (int i = 1; i <= 4; i++)
            {
                berths.Add(new Berth { TerminalId = t.Id, Code = $"{t.Code}-B{i}", LengthMeters = 350 + i*10, MaxDraftMeters = 14 + i });
            }
        }
        context.Berths.AddRange(berths);
        await context.SaveChangesAsync();

        // Shipping lines
        var lines = new List<ShippingLine>
        {
            new() { Code = "MAEU", Name = "Maersk Line" },
            new() { Code = "MSCU", Name = "MSC" },
            new() { Code = "CMA",  Name = "CMA CGM" },
            new() { Code = "HLC",  Name = "Hapag-Lloyd" }
        };
        context.ShippingLines.AddRange(lines);
        await context.SaveChangesAsync();

        // Vessels
        var vessels = new List<Vessel>();
        int vcount = 0;
        foreach (var l in lines)
        {
            for (int i = 0; i < 2; i++)
            {
                vcount++;
                vessels.Add(new Vessel { ShippingLineId = l.Id, Name = $"Vessel-{vcount}", ImoNumber = $"IMO{9000000 + vcount:000000}", GrossTonnage = 80000 + random.Next(20000) });
            }
        }
        context.Vessels.AddRange(vessels);
        await context.SaveChangesAsync();

        // Bookings
        var bookings = new List<BerthBooking>();
        for (int b = 0; b < 60; b++)
        {
            var berth = berths[random.Next(berths.Count)];
            var vessel = vessels[random.Next(vessels.Count)];
            var line = lines.First(x => x.Id == vessel.ShippingLineId);
            var arrival = now.AddDays(-90 + random.Next(120));
            var departure = arrival.AddHours(12 + random.Next(36));
            bookings.Add(new BerthBooking
            {
                BerthId = berth.Id,
                VesselId = vessel.Id,
                ShippingLineId = line.Id,
                ArrivalTime = arrival,
                DepartureTime = departure,
                Status = BookingStatus.Completed
            });
        }
        context.BerthBookings.AddRange(bookings);
        await context.SaveChangesAsync();

        // Containers
        var containers = new List<Container>();
        for (int c = 0; c < 400; c++)
        {
            var booking = bookings[random.Next(bookings.Count)];
            var type = (ContainerType)(1 + random.Next(5));
            containers.Add(new Container
            {
                ContainerNumber = GenerateContainerNumber(random),
                Type = type,
                BerthBookingId = booking.Id,
                ShippingLineId = booking.ShippingLineId,
                GateInTime = booking.ArrivalTime.AddHours(random.Next(-2, 4)),
                GateOutTime = random.Next(0,100) < 70 ? booking.DepartureTime.AddHours(random.Next(-1,48)) : null,
                IsDeleted = false
            });
        }
        context.Containers.AddRange(containers);
        await context.SaveChangesAsync();

        // Inspections
        var inspections = new List<CustomsInspection>();
        for (int i = 0; i < 100; i++)
        {
            var cont = containers[random.Next(containers.Count)];
            inspections.Add(new CustomsInspection
            {
                ContainerId = cont.Id,
                InspectionDate = cont.GateInTime.AddHours(random.Next(2, 24)),
                Outcome = (InspectionOutcome)(1 + random.Next(4)),
                Inspector = $"INSP-{random.Next(100):000}"
            });
        }
        context.CustomsInspections.AddRange(inspections);
        await context.SaveChangesAsync();
    }

    private static string GenerateContainerNumber(Random random)
    {
        string[] owners = new[] { "MSKU", "MAEU", "CMAU", "HLBU" };
        string owner = owners[random.Next(owners.Length)];
        int num = random.Next(100000, 999999);
        return $"{owner}{num}0";
    }
}
