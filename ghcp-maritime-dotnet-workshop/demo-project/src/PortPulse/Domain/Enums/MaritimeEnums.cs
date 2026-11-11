namespace PortPulse.Domain.Enums;

public enum ContainerType
{
    Dry = 1,
    Reefer = 2,
    Tank = 3,
    FlatRack = 4,
    OpenTop = 5
}

public enum BookingStatus
{
    Planned = 1,
    Confirmed = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5
}

public enum InspectionOutcome
{
    Pending = 1,
    Passed = 2,
    Hold = 3,
    Rejected = 4
}
