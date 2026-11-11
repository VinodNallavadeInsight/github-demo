namespace PortPulse.Features.Vessels;

public record VesselDto(Guid Id, Guid ShippingLineId, string Name, string ImoNumber, int GrossTonnage);
public record CreateVesselDto(Guid ShippingLineId, string Name, string ImoNumber, int GrossTonnage);
