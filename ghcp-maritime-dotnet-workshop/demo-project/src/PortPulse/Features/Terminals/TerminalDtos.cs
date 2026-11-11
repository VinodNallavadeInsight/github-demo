namespace PortPulse.Features.Terminals;

public record TerminalDto(Guid Id, string Code, string Name, string Location);
public record CreateTerminalDto(string Code, string Name, string Location);
public record UpdateTerminalDto(string Code, string Name, string Location);
