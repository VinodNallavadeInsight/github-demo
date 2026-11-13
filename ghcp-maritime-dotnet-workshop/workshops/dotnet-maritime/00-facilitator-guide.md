# Facilitator Guide - GitHub Copilot for .NET Developers (Maritime Logistics Track)

## 1. Overview
- **Audience**: 15 developers
- **Duration**: 4 sessions × 2.5 hours = 10 hours
- **Format**: Instructor-led demos + hands-on labs
- **Environment**: VS Code/Visual Studio, GitHub Copilot, .NET 8, Docker (SQL Server)

## 2. Timing & Agenda
| Session | Time | Focus |
|---------|------|-------|
| Session 1: Foundations | 150 min | LLM basics, Copilot surfaces, inline/chat, tests, legacy refactor (M1-A to M1-D) |
| Session 2: Prompt Craft | 150 min | GRACE, few-shot, prompt files, Moq tests (M2-A to M2-C) |
| Session 3: Agents and Review | 150 min | Agent mode, bug hunting, code review (M3-A to M3-C) |
| Session 4: Enterprise Delivery | 150 min | Models, MCP, spec-driven, cloud agent (M4-A to M4-D) |

Breaks: 10-15 min mid-session recommended.

## 3. Room Setup
- Projector/screen for demos
- Attendees have laptops with prerequisites met
- GitHub Copilot licenses active
- Network access to GitHub/Microsoft
- Docker available if using SQL Server
- Workshop repo accessible (local or cloned)

## 4. Before Each Session
- [ ] Verify repo is up to date
- [ ] Run `dotnet build` and `dotnet test` in `demo-project/`
- [ ] Test Copilot inline/chat/agent
- [ ] Have demo markers ready (see `docs/demo-marker-index.md`)
- [ ] Prepare answer keys for planted bugs/flaws

## 5. Session Facilitation Tips
- **Show, then do**: Demo first, then have attendees repeat
- **Review AI output**: Always discuss why accepting/rejecting
- **Use failures**: If suggestion is off-track, teach recovery
- **Encourage experimentation**: Try different prompts
- **Time management**: Labs have checkpoints - adjust as needed

## 6. Answer Key

### M3-A: BerthUtilizationCalculator - Subtle Bug
**Location**: `demo-project/src/PortPulse/Features/Analytics/BerthUtilizationCalculator.cs` (planted)

**Bug**: A booking that partially overlaps the date-range edges is not clamped to the window.
- If booking starts before `rangeStart`, effective start should be `rangeStart`
- If booking ends after `rangeEnd`, effective end should be `rangeEnd`
- Utilization calculation must use clamped interval, not full booking duration

**Failing Scenario**:
```csharp
// Range: 2025-10-01 to 2025-10-07 (7 days)
// Booking: 2025-09-28 00:00 to 2025-10-03 00:00 (overlaps start)
// Incorrect: uses full overlap ignoring clamp -> wrong hours/days
// Correct: clamp to [2025-10-01, 2025-10-03]
```

**How to guide**: Ask agent to trace with specific dates, check boundary conditions, write unit test that fails.

### M3-C: BookingService.CancelAsync - Flaws
**Location**: `demo-project/src/PortPulse/Features/Bookings/BookingService.cs`

**Flaws to identify**:
1. **Missing CancellationToken** - `FirstOrDefaultAsync(b => b.Id == id)` called without `ct`
2. **No concurrency handling** - Uses `RowVersion` on `BerthBooking` but no `DbUpdateConcurrencyException` handling
3. **No logging** - No structured logging of cancellation attempts
4. **No null/status validation** - Doesn't check if already cancelled/completed, no guard clauses
5. **No authorization/context** - No user/idempotency considerations (real-world)
6. **No transaction/side effects** - May need to handle related entities

**Review Prompt**: "Review CancelAsync for correctness, concurrency, error handling, and logging. List specific issues with fixes."

## 7. Risk & Fallback Plans
| Risk | Impact | Mitigation |
|------|--------|------------|
| No internet/Copilot down | High | Have pre-generated suggestions, use offline examples, focus on prompting concepts |
| Docker/SQL issues | Medium | Use InMemory for demos, skip DB-dependent parts, use seed data in memory |
| Copilot gives wrong API | Medium | Teach review - compare with existing codebase patterns |
| Time running short | Medium | Prioritize M1-A/B, M2-A, M3-A demos; make labs optional |
| Environment differences | Medium | Use `dotnet build/test` as verification, stick to repo patterns |

## 8. Demo Script Notes
- **M1-A**: Let them struggle a bit with validation logic, guide with comments
- **M1-D**: Emphasize security (SQL injection) - very important
- **M2-A**: Force use of GRACE framework, compare weak vs strong prompts
- **M3-A**: Don't reveal bug - let agent discover it
- **M4-C**: Show spec-driven - agent reads full spec and implements comprehensively

## 9. Post-Session
- [ ] Collect feedback
- [ ] Share repo link/resources
- [ ] Highlight key takeaways
- [ ] Encourage continued practice with backlog items

## 10. Additional Resources
- `docs/demo-marker-index.md` - All markers
- `docs/prompting-framework.md` - GRACE
- `workshops/dotnet-maritime/` - All session guides
- `workshops/dotnet-maritime/05-github-push-and-pr-review.md` - Supplemental GitHub push and Copilot PR review demo
- `docs/backlog/` - Practice exercises
