# Workshop 03: Agents and Review

**Duration**: 60 min | **Level**: Advanced

## Objectives
- Use Agent Mode effectively
- Find subtle bugs with systematic reasoning
- Review code and identify flaws
- Add features with agent assistance

## Block 1: Find the Planted Bug (M3-A) (20 min)

**Marker**: M3-A - BerthUtilizationCalculator

Bug: booking partially overlapping date-range edges not clamped to window. 
Do NOT mention bug. Ask agent: "Analyze BerthUtilizationCalculator for correctness. Check edge cases around date boundaries. Show failing scenario."

Look at how utilization is calculated - if booking starts before range start or ends after range end, it must be clamped.

## Block 2: Agent Mode Feature (M3-B) (20 min)

**Marker**: M3-B - Container search endpoint

Use Agent Mode with task: "Add container search endpoint with filters (container number partial, shipping line, type, date range), sorting, paging. Follow project conventions."

Let agent explore codebase, create files, update Program if needed.

## Block 3: Code Review Flaws (M3-C) (15 min)

**Marker**: M3-C - BookingService.CancelAsync

Review CancelAsync:
```csharp
public async Task<bool> CancelAsync(Guid id, CancellationToken ct)
{
    var entity = await _db.BerthBookings.FirstOrDefaultAsync(b => b.Id == id);
    if (entity == null) return false;
    entity.Status = BookingStatus.Cancelled;
    await _db.SaveChangesAsync(ct);
    return true;
}
```

Flaws: missing CancellationToken in FirstOrDefaultAsync, no concurrency handling (RowVersion), no logging, no null check guard, no validation of current status.

## Recap (5 min)
