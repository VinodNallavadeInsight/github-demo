# Project Overview - Northgate Maritime Group & PortPulse

## Client Profile
**Company**: Northgate Maritime Group (fictional client for this workshop)  
**Industry**: Maritime Logistics & Container Terminal Operations  
**Headquarters**: Rotterdam, Netherlands (with terminals in Hamburg and Antwerp)  
**Size**: Mid-large enterprise operating multiple container terminals across Europe  

## Business Challenge
Northgate Maritime Group operates three major container terminals and needs to modernize their internal terminal operations platform. Their current systems are fragmented, making it difficult to:
- Track berth bookings and vessel schedules efficiently
- Monitor container dwell times and demurrage charges
- Generate operational reports for terminal managers
- Ensure consistent, secure development practices across their .NET teams

## What We're Building
**PortPulse** - A modern terminal operations management system built with .NET 8. This is a **demonstration application** designed specifically for GitHub Copilot training.

### Core Features
- **Terminal & Berth Management** - Track terminals, berths, and their capacity
- **Vessel & Shipping Line Operations** - Manage vessels, shipping lines, and schedules
- **Berth Booking System** - Schedule vessels with conflict detection and concurrency control
- **Container Tracking** - Track containers from gate-in to gate-out with soft-delete support
- **Customs Inspections** - Monitor inspection workflows and outcomes
- **Analytics & Reporting** - Dwell times, berth utilization, vessel turnaround, top inspected lines
- **Demurrage Billing** - Calculate charges based on free days, tiered rates, and weekend rules
- **Legacy Codebase** - Intentionally included for refactoring practice

### Technical Stack
- ASP.NET Core 8 Minimal APIs
- Entity Framework Core 8 with SQL Server
- Blazor Server Dashboard
- xUnit, Moq for testing
- Parameterized ADO.NET for reporting

## Purpose of This Demo
This is **training/demo code** - not production software. It's purpose-built for hands-on GitHub Copilot labs where participants will:
- Use Copilot for code completion and Chat
- Practice prompt engineering with real scenarios
- Leverage Agent Mode to find bugs and add features
- Learn to refactor legacy code safely
- Implement features using spec-driven development
- Apply enterprise patterns with Copilot guidance

All demo markers (M1-A through M4-D) are planted intentionally to teach specific Copilot capabilities in a realistic maritime context.
