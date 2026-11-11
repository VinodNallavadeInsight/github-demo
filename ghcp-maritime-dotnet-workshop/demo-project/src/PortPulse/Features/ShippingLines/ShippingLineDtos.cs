namespace PortPulse.Features.ShippingLines;

public record ShippingLineDto(Guid Id, string Code, string Name);
public record CreateShippingLineDto(string Code, string Name);
