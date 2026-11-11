# Workshop 01: Foundations - Getting Started with GitHub Copilot

**Duration**: 60 min | **Level**: Beginner | **Prerequisites**: VS Code + Copilot extension installed

## Workshop Series Roadmap

| # | Workshop | Focus |
|---|----------|-------|
| 01 | Foundations (This) | Copilot role, inline, Chat, security, customization |
| 02 | Prompt Craft | CORE/GRACE prompting, comment & few-shot techniques, testing |
| 03 | Agents and Review | Agent Mode, code review, finding bugs, systematic reasoning |
| 04 | Enterprise Delivery | Model selection, MCP, spec-driven development, Coding Agent |

## Block 1: Project Overview (10 min)

Navigate to the demo project:
```bash
cd ghcp-maritime-dotnet-workshop/demo-project
dotnet build
dotnet test
```

Explore the PortPulse solution - Domain, Data, Features (vertical slices), Legacy, Components.

## Block 2: Inline Suggestions - DEMO (10 min)

**Marker**: M1-A - ContainerNumberValidator.cs

1. Open `src/PortPulse/Features/Containers/ContainerNumberValidator.cs`
2. Notice the method throws `NotImplementedException` with `// [DEMO M1-A] short description`
3. Start typing a comment above Validate: `// Validate ISO 6346 container number check digit`
4. Let Copilot suggest implementation. Accept with Tab, review.
5. Try cycling alternatives (Alt+] / Alt+[) and word-by-word (Ctrl+→)

**Exercise**: Add basic validation logic for container number format.

## Block 3: Chat Panel - Explain Code (10 min)

**Marker**: M1-B - BerthConflictDetector.cs

1. Open `src/PortPulse/Features/Analytics/BerthConflictDetector.cs`
2. Select the HasConflict method (dense LINQ with short names)
3. Open Chat (Ctrl+Alt+I) - Ask: "Explain this code step by step. What does it do?"
4. Follow up: "Is there a clearer way to write this?"
5. Follow up: "Are there edge cases?"

**Teaching Points**: Selection gives context, follow-ups build on conversation.

## Block 4: Generate Tests (10 min)

**Marker**: M1-C - DemurrageCalculator.cs

1. Open `src/PortPulse/Features/Billing/DemurrageCalculator.cs`
2. Select the class/method
3. Ask Chat: "Generate xUnit tests for DemurrageCalculator with edge cases (weekends, zero chargeable days, partial weeks)"
4. Create a test file in `tests/PortPulse.Tests/Unit/Billing/DemurrageCalculatorTests.cs`
5. Run tests - they should fail initially? Or pass. Note: "NO unit tests" per requirements - you add them.
6. Review generated tests, adjust as needed.

## Block 5: Refactor Legacy Code (12 min)

**Marker**: M1-D - LegacyDwellReport.cs and LegacyEndpoints.cs

1. Open `src/PortPulse/Legacy/LegacyDwellReport.cs`
2. Identify issues: string concatenation SQL, .Result, N+1, ToList() before Where, DateTime.Now, swallowed exceptions, magic numbers, 100-line method
3. Use Agent Mode or Chat: "Refactor LegacyDwellReport to use parameterized SQL via SqlClient, async/await properly, fix N+1, remove magic numbers, add logging, handle exceptions"
4. Apply fixes incrementally, build after each change
5. Check LegacyEndpoints - it's debug-only

**Key**: Never concatenate SQL. Always use parameters.

## Recap (8 min)

- Inline: completions, cycling, word-by-word
- Chat: explain, follow-ups, generate code
- Refactor legacy safely with tests
- Always review AI output

