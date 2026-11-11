using System.ComponentModel.DataAnnotations;

namespace PortPulse.Domain.Entities;

public class Terminal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public ICollection<Berth> Berths { get; set; } = new List<Berth>();
}

public class Berth
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TerminalId { get; set; }
    public Terminal Terminal { get; set; } = null!;
    public string Code { get; set; } = string.Empty;
    public int LengthMeters { get; set; }
    public int MaxDraftMeters { get; set; }
    public ICollection<BerthBooking> Bookings { get; set; } = new List<BerthBooking>();
}

public class ShippingLine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<Vessel> Vessels { get; set; } = new List<Vessel>();
    public ICollection<BerthBooking> Bookings { get; set; } = new List<BerthBooking>();
}

public class Vessel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ShippingLineId { get; set; }
    public ShippingLine ShippingLine { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string ImoNumber { get; set; } = string.Empty;
    public int GrossTonnage { get; set; }
    public ICollection<BerthBooking> Bookings { get; set; } = new List<BerthBooking>();
}

public class BerthBooking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BerthId { get; set; }
    public Berth Berth { get; set; } = null!;
    public Guid VesselId { get; set; }
    public Vessel Vessel { get; set; } = null!;
    public Guid ShippingLineId { get; set; }
    public ShippingLine ShippingLine { get; set; } = null!;
    public DateTimeOffset ArrivalTime { get; set; }
    public DateTimeOffset DepartureTime { get; set; }
    public PortPulse.Domain.Enums.BookingStatus Status { get; set; } = PortPulse.Domain.Enums.BookingStatus.Planned;
    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    public ICollection<Container> Containers { get; set; } = new List<Container>();
}

public class Container
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContainerNumber { get; set; } = string.Empty;
    public PortPulse.Domain.Enums.ContainerType Type { get; set; } = PortPulse.Domain.Enums.ContainerType.Dry;
    public Guid BerthBookingId { get; set; }
    public BerthBooking BerthBooking { get; set; } = null!;
    public Guid ShippingLineId { get; set; }
    public ShippingLine ShippingLine { get; set; } = null!;
    public DateTimeOffset GateInTime { get; set; }
    public DateTimeOffset? GateOutTime { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<CustomsInspection> Inspections { get; set; } = new List<CustomsInspection>();
}

public class CustomsInspection
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ContainerId { get; set; }
    public Container Container { get; set; } = null!;
    public DateTimeOffset InspectionDate { get; set; }
    public PortPulse.Domain.Enums.InspectionOutcome Outcome { get; set; } = PortPulse.Domain.Enums.InspectionOutcome.Pending;
    public string Inspector { get; set; } = string.Empty;
}

public class DemurrageCharge
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ContainerId { get; set; }
    public Container Container { get; set; } = null!;
    public DateTimeOffset CalculatedAt { get; set; }
    public decimal FreeDays { get; set; }
    public decimal DailyRate { get; set; }
    public decimal TotalCharge { get; set; }
    public string Notes { get; set; } = string.Empty;
}
