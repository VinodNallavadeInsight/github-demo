namespace PortPulse.Features.Billing;

public class DemurrageCalculator
{
    public decimal Calculate(DateTimeOffset gateIn, DateTimeOffset gateOut, int freeDays, decimal dailyRate, bool includeWeekends)
    {
        var totalDays = (gateOut.Date - gateIn.Date).TotalDays;
        if (totalDays <= freeDays) return 0m;
        var chargeable = totalDays - freeDays;
        if (!includeWeekends)
        {
            var days = 0;
            for (var d = gateIn.Date.AddDays(freeDays + 1); d <= gateOut.Date; d = d.AddDays(1))
            {
                if (d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday) days++;
            }
            chargeable = days;
        }
        return (decimal)chargeable * dailyRate;
    }
}
