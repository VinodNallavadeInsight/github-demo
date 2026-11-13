# GitHub Copilot for .NET Developers - Maritime Logistics Track (ghcp-maritime-dotnet-workshop)

This repository is a complete GitHub Copilot training workshop built around **PortPulse**, a realistic .NET 8 demo application for container terminal operations at the fictional **Northgate Maritime Group**. It includes the demo app, Copilot customizations, session guides, and ready-to-paste backlog items.

## Table of Contents
- [Overview](#overview)
- [Target Audience](#target-audience)
- [Solution Structure](#solution-structure)
- [PortPulse Demo Application](#portpulse-demo-application)
- [Getting Started](#getting-started)
- [Database & Migrations](#database--migrations)
- [Running & Testing](#running--testing)
- [GitHub Copilot Customizations](#github-copilot-customizations)
- [Workshop Sessions](#workshop-sessions)
- [Demo Markers (M1-A through M4-D)](#demo-markers-m1-a-through-m4-d)
- [Documentation Index](#documentation-index)
- [Engagement Brief](#engagement-brief)
- [Quality & Conventions](#quality--conventions)
- [Troubleshooting](#troubleshooting)
- [License](#license)

## Overview
- **Workshop Duration**: 4 sessions × 2.5 hours (10 hours total)
- **Target Framework**: .NET 8 (LTS), C# 12
- **Language Features**: Nullable reference types, Implicit usings enabled
- **Focus**: GitHub Copilot across completions, Chat, Agent Mode, prompt files, instructions, MCP, model selection, and spec-driven/cloud agents

## Target Audience
.NET developers working in maritime/logistics or enterprise teams looking to adopt GitHub Copilot effectively with secure, maintainable patterns.

## Solution Structure
```
ghcp-maritime-dotnet-workshop/
├── .github/
│   ├── copilot-instructions.md
│   ├── instructions/   (ef-core.instructions.md, api-endpoints.instructions.md, tests.instructions.md)
│   └── prompts/        (add-endpoint.prompt.md, generate-tests.prompt.md, explain-linq.prompt.md)
├── demo-project/
│   ├── PortPulse.sln
│   ├── docker-compose.yml
│   ├── .vscode/mcp.json.example
│   ├── src/PortPulse/
│   │   ├── Domain/ (Entities, Enums)
│   │   ├── Data/ (DbContext, Configurations, Migrations, Seed)
│   │   ├── Features/ (Terminals, Berths, Vessels, ShippingLines, Bookings, Containers, Analytics, Billing)
│   │   ├── Infrastructure/Sql/ (parameterized ADO.NET reporting)
│   │   ├── Legacy/ (intentionally poor code for refactoring)
│   │   └── Components/ (Blazor Server dashboard)
│   ├── tests/PortPulse.Tests/ (Unit/, Data/)
│   ├── specs/demurrage-billing.md
│   ├── AGENTS.md
│   └── README.md
├── workshops/dotnet-maritime/
│   ├── 00-prerequisites.md, 00-facilitator-guide.md
│   ├── 01-foundations.md, 02-prompt-craft.md, 03-agents-and-review.md, 04-enterprise-delivery.md
├── docs/
│   ├── workshop-overview.md, session-plans.md, architecture.md, data-model.md
│   ├── demo-marker-index.md, prompting-framework.md, copilot-features-guide.md
│   └── backlog/ (01-*.md through 06-*.md)
├── engagement-brief.md
├── .gitignore, .editorconfig, Directory.Build.props
└── README.md
```

## PortPulse Demo Application
PortPulse manages container terminal operations with realistic maritime vocabulary (TEU, berth, demurrage, dwell time, IMO number). Key areas:
- **Domain**: Terminal, Berth, ShippingLine, Vessel, BerthBooking (with RowVersion), Container (soft-delete with global query filter), CustomsInspection, DemurrageCharge
- **Data**: EF Core 8 + SQL Server, IEntityTypeConfiguration, indexes, unique constraints (IMO, container number), decimal precision, initial migration, deterministic seed (Development only)
- **Features**: Vertical slices with service interfaces/implementations, DTOs, endpoint-group extensions, async/await + CancellationToken, AsNoTracking, DTO projection, ProblemDetails
- **Analytics/Billing**: Rich LINQ examples, DemurrageCalculator (pure class)
- **Infrastructure/Sql**: Parameterized ADO.NET reporting + FromSqlInterpolated example (no string-concatenated SQL except Legacy)
- **Legacy**: Intentionally bad code for refactoring demos (string concatenation, sync-over-async, N+1, swallowed exceptions, magic numbers)
- **Components**: Blazor Server dashboard pages

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [Visual Studio Code](https://code.visualstudio.com/) with [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) extension (or Visual Studio 2022)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) for SQL Server container (optional but recommended)

### Installation
```bash
# Clone the repository
git clone <repo-url>
cd ghcp-maritime-dotnet-workshop

# Restore and build
cd demo-project
dotnet restore
dotnet build
dotnet test
```

### SQL Server (Optional)
```bash
# Set environment variable (PowerShell)
$env:MSSQL_SA_PASSWORD="YourStrong!Passw0rd"

# Start SQL Server 2022
docker compose up -d

# Connection string comes from configuration/environment/user-secrets (never hard-coded)
# Update appsettings or use User Secrets as needed
```

## Database & Migrations
Migrations are included (`src/PortPulse/Data/Migrations/InitialCreate`). To add/update:
```bash
# Add migration
dotnet ef migrations add <MigrationName> --project src/PortPulse --startup-project src/PortPulse --output-dir Data/Migrations

# Update database
dotnet ef database update --project src/PortPulse --startup-project src/PortPulse
```
Seeding runs only in Development with a deterministic fixed seed.

## Running & Testing
```bash
# Run the web app
dotnet run --project src/PortPulse

# Run all tests
dotnet test

# Build solution
dotnet build
```

## GitHub Copilot Customizations
- **`.github/copilot-instructions.md`** - Project-wide rules (async+CancellationToken, AsNoTracking/DTO projection, parameterized SQL only, TimeProvider, UTC, no secrets in logs, System.Text.Json, ProblemDetails, structured logging, Do Not list)
- **`.github/instructions/ef-core.instructions.md`** (`applyTo: demo-project/src/PortPulse/Data/**/*.cs`) - EF Core conventions
- **`.github/instructions/api-endpoints.instructions.md`** (`applyTo: demo-project/src/PortPulse/Features/**/Endpoints.cs`) - API patterns
- **`.github/instructions/tests.instructions.md`** (`applyTo: demo-project/tests/**/*.cs`) - Testing rules
- **`.github/prompts/add-endpoint.prompt.md`** - Guided endpoint creation
- **`.github/prompts/generate-tests.prompt.md`** - Test generation with Moq
- **`.github/prompts/explain-linq.prompt.md`** - LINQ explanation
- **`demo-project/AGENTS.md`** - Quick reference for agents/build/test/migrations/conventions

## Workshop Sessions
| Session | File | Focus | Markers |
|---------|------|-------|---------|
| 00 | `workshops/dotnet-maritime/00-prerequisites.md` | Setup, VS Code/VS, Copilot sign-in, SQL, checklist | - |
| 00 | `workshops/dotnet-maritime/00-facilitator-guide.md` | Timing, risks, answer key (bug/flaws) | M3-A, M3-C |
| 01 | `workshops/dotnet-maritime/01-foundations.md` | Completions, Chat explain, tests, legacy refactor | M1-A, M1-B, M1-C, M1-D |
| 02 | `workshops/dotnet-maritime/02-prompt-craft.md` | GRACE, few-shot, prompt files | M2-A, M2-B, M2-C |
| 03 | `workshops/dotnet-maritime/03-agents-and-review.md` | Agent mode, bug hunting, code review | M3-A, M3-B, M3-C |
| 04 | `workshops/dotnet-maritime/04-enterprise-delivery.md` | Models, MCP, spec-driven, cloud agent | M4-A, M4-B, M4-C, M4-D |
| Supplemental | `workshops/dotnet-maritime/05-github-push-and-pr-review.md` | Push a change, open a PR, and review with GitHub Copilot | - |

## Demo Markers (M1-A through M4-D)
| Marker | Location | Session | Purpose | Copilot Feature |
|--------|----------|---------|---------|-----------------|
| M1-A | `demo-project/src/PortPulse/Features/Containers/ContainerNumberValidator.cs` | 1 | ISO 6346 validation (throws NotImplementedException) | Completions/Chat |
| M1-B | `demo-project/src/PortPulse/Features/Analytics/BerthConflictDetector.cs` | 1 | Dense LINQ - explain | Chat explain |
| M1-C | `demo-project/src/PortPulse/Features/Billing/DemurrageCalculator.cs` | 1 | No unit tests - generate with edge cases | Chat/Agent tests |
| M1-D | `demo-project/src/PortPulse/Legacy/LegacyDwellReport.cs`, `demo-project/src/PortPulse/Legacy/LegacyEndpoints.cs` | 1 | Bad code - refactor | Agent/Chat refactor |
| M2-A | `docs/backlog/01-vessel-arrivals.md` | 2 | Add endpoint with GRACE | Prompting framework |
| M2-B | `demo-project/src/PortPulse/Features/Terminals/TerminalService.cs`, `Berths/BerthService.cs` (few-shot) | 2 | Create ShippingLineQueryService | Few-shot/prompt files |
| M2-C | `demo-project/src/PortPulse/Features/Bookings/BookingService.cs` | 2 | No tests - generate with Moq | generate-tests prompt |
| M3-A | `demo-project/src/PortPulse/Features/Analytics/BerthUtilizationCalculator.cs` | 3 | Subtle bug (edge clamping) - find it | Agent reasoning |
| M3-B | `docs/backlog/02-container-search.md` | 3 | Agent mode feature | Agent mode |
| M3-C | `demo-project/src/PortPulse/Features/Bookings/BookingService.cs` (CancelAsync) | 3 | Review flaws | Code review |
| M4-A | `demo-project/src/PortPulse/Features/Analytics/DwellTimeAnalytics.cs` | 4 | Model comparison | Model choice |
| M4-B | `demo-project/.vscode/mcp.json.example` | 4 | MCP (GitHub + SQL placeholder) | MCP |
| M4-C | `demo-project/specs/demurrage-billing.md` | 4 | Spec-driven development | Agent spec-driven |
| M4-D | `docs/backlog/*.md` | 4 | Cloud agent with backlog | Cloud agent |

See `docs/demo-marker-index.md` for full details.

## Documentation Index
- `docs/workshop-overview.md` - Goals, audience, outcomes, roadmap
- `docs/session-plans.md` - Timed agendas, labs, discussion questions
- `docs/architecture.md` - Layers, request flow, EF Core/SQL
- `docs/data-model.md` - Entities/relationships with Mermaid ER diagram
- `docs/prompting-framework.md` - GRACE framework with examples
- `docs/copilot-features-guide.md` - All Copilot features with .NET examples
- `docs/demo-marker-index.md` - Complete marker reference
- `docs/backlog/` - 6 ready-to-paste GitHub issue drafts

## Engagement Brief
`engagement-brief.md` contains fictional client profile (Northgate Maritime Group), goals, constraints, and success measures.

## Quality & Conventions
- **Build/Test**: `dotnet build` and `dotnet test` pass (solution configured for .NET 8)
- **Async**: Use async/await with CancellationToken everywhere
- **Data Access**: AsNoTracking for reads, DTO projection with Select
- **SQL**: Parameterized only (no string concatenation except Legacy)
- **Time**: Inject TimeProvider, use UTC for stored dates
- **Errors**: ProblemDetails
- **Security**: No secrets/connection strings in logs
- **Serialization**: System.Text.Json
- **Maritime Terms**: TEU, berth, demurrage, dwell time, IMO number

## Troubleshooting
- **EF Core tools**: Install `dotnet tool install --global dotnet-ef --version 8.0.10`
- **SQL connection**: Use configuration/env vars/user-secrets; `docker-compose.yml` expects `MSSQL_SA_PASSWORD`
- **Migrations**: If design-time issues occur, ensure startup project is set correctly
- **Build errors**: Run `dotnet build` and fix all errors before proceeding

## License
This is a training repository created for educational purposes.
