namespace PortPulse.Features.Analytics;

public class BerthConflictDetector
{
    public bool HasConflict(IEnumerable<(DateTimeOffset s, DateTimeOffset e)> a, IEnumerable<(DateTimeOffset s, DateTimeOffset e)> b)
    {
        foreach (var x in a) foreach (var y in b) if (!(x.e <= y.s || y.e <= x.s)) return true;
        return false;
    }
}
