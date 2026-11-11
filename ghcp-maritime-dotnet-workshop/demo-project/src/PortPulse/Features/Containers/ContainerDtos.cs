namespace PortPulse.Features.Containers;

public record ContainerDto(Guid Id, string ContainerNumber, string Type, Guid ShippingLineId, DateTimeOffset GateInTime, DateTimeOffset? GateOutTime);
