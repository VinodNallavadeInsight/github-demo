# Workshop 02: Prompt Craft

**Duration**: 60 min | **Level**: Intermediate

## Objectives
- Use GRACE framework (Goal, Role, Artifacts, Constraints, Examples)
- Apply few-shot prompting
- Use prompt files
- Generate Moq tests

## Block 1: GRACE Framework (10 min)

Review `docs/prompting-framework.md`. Practice with weak vs strong prompts.

## Block 2: Add Endpoint with GRACE (M2-A) (15 min)

**Marker**: M2-A - Add "vessel arrivals this week" endpoint

Use this GRACE prompt:
```
Goal: Add a GET /api/analytics/vessel-arrivals-this-week endpoint returning vessels arriving in next 7 days
Role: Senior .NET API developer
Artifacts: Use existing patterns (features with service/interface/DTOs/endpoints)
Constraints: async/await, CancellationToken, AsNoTracking, DTO projection, ProblemDetails
Examples: Look at Terminals/Berths endpoints and services
```

Create: DTOs, service method, endpoint, register.

## Block 3: Few-Shot (M2-B) (15 min)

**Marker**: M2-B - Create ShippingLineQueryService

Look at `TerminalService` and `BerthService` as few-shot examples. Ask Copilot to create `ShippingLineQueryService` in same style with GetAll/GetById.

## Block 4: Moq Tests (M2-C) (15 min)

**Marker**: M2-C - BookingService has no tests

Use `.github/prompts/generate-tests.prompt.md`:
```
/generate-tests className=BookingService targetPath=tests/PortPulse.Tests/Unit/Bookings/BookingServiceTests.cs
```
Or prompt directly with Moq, test CreateAsync overlap, CancelAsync etc.

## Recap (5 min)
- GRACE improves results
- Few-shot leverages existing patterns
- Prompt files standardize tasks
