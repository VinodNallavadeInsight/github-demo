namespace PortPulse.Features.Berths;

public record BerthDto(Guid Id, Guid TerminalId, string Code, int LengthMeters, int MaxDraftMeters);
public record CreateBerthDto(Guid TerminalId, string Code, int LengthMeters, int MaxDraftMeters);
public record UpdateBerthDto(string Code, int LengthMeters, int MaxDraftMeters);
