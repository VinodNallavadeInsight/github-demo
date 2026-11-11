namespace PortPulse.Features.Bookings;

public record BookingDto(Guid Id, Guid BerthId, Guid VesselId, Guid ShippingLineId, DateTimeOffset ArrivalTime, DateTimeOffset DepartureTime, string Status);
public record CreateBookingDto(Guid BerthId, Guid VesselId, Guid ShippingLineId, DateTimeOffset ArrivalTime, DateTimeOffset DepartureTime);
