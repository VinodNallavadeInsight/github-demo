# Prerequisites - GitHub Copilot for .NET Developers (Maritime Logistics Track)

This guide covers everything you need before attending the workshop. Complete all steps and use the verification checklist at the end.

## 1. Hardware & Software Requirements
| Requirement | Minimum | Recommended |
|-------------|---------|-------------|
| OS | Windows 10/11, macOS 12+, Linux (Ubuntu 22.04+) | Same with latest updates |
| RAM | 8 GB | 16 GB+ |
| Disk Space | 10 GB free | 20 GB+ |
| CPU | 64-bit dual-core | Quad-core+ |

## 2. Install Required Tools

### 2.1 .NET 8 SDK
Download and install from [dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0).

Verify installation:
```bash
dotnet --version
# Expected: 8.0.x
dotnet --list-sdks
```

### 2.2 Git
Download from [git-scm.com/downloads](https://git-scm.com/downloads).

Verify:
```bash
git --version
```

### 2.3 Visual Studio Code (Recommended)
Download from [code.visualstudio.com](https://code.visualstudio.com/).

Required extensions:
- [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot)
- [GitHub Copilot Chat](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat)
- [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) (optional but recommended)
- [Docker](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker) (optional)

### 2.4 Visual Studio 2022 (Alternative)
[Visual Studio 2022](https://visualstudio.microsoft.com/vs/) version 17.8+ with workloads:
- ASP.NET and web development
- .NET desktop development (if needed)

Ensure GitHub Copilot is installed/enabled.

### 2.5 Docker Desktop
Download from [docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop/).
Required if you want to run SQL Server locally via `docker-compose.yml`.

### 2.6 EF Core CLI Tools
```bash
dotnet tool install --global dotnet-ef --version 8.0.10
dotnet ef --version
# Expected: Entity Framework Core .NET Command-line Tools 8.0.10
```
Update if needed: `dotnet tool update --global dotnet-ef --version 8.0.10`

## 3. GitHub Copilot Setup

1. Sign in to GitHub with an account that has **GitHub Copilot** access (Individual, Business, or Enterprise).
2. In VS Code: `Ctrl+Shift+P` / `Cmd+Shift+P` → `GitHub Copilot: Sign In`
3. In Visual Studio: Sign in with GitHub account that has Copilot license
4. Verify Copilot icon is active in status bar
5. Test inline suggestions by typing in a C# file

## 4. Workshop Repository Setup

### 4.1 Clone
```bash
git clone <your-fork-or-repo-url>
cd ghcp-maritime-dotnet-workshop
```

### 4.2 Build and Test
```bash
cd demo-project
dotnet restore
dotnet build
dotnet test
```
All tests should pass.

### 4.3 SQL Server Setup (Optional)
1. Create/set password:
```bash
# PowerShell
$env:MSSQL_SA_PASSWORD="YourStrong!Passw0rd"
# Bash/Zsh
export MSSQL_SA_PASSWORD="YourStrong!Passw0rd"
```
2. Start container:
```bash
docker compose up -d
```
3. Verify: `docker ps` should show `portpulse-sql`
4. Connection string is read from configuration/environment variables or user-secrets (never hard-coded). See `demo-project/README.md`.

> **Note**: The app can run without SQL Server for many demos; EF Core is configured with SQL Server provider and migrations are included.

## 5. Recommended VS Code Settings
Create `.vscode/settings.json` in workspace if desired:
```json
{
  "github.copilot.enable": {
    "*": true
  },
  "editor.inlineSuggest.enabled": true
}
```

## 6. Verification Checklist
- [ ] `.NET 8 SDK` installed and `dotnet --version` shows 8.0.x
- [ ] `git` installed
- [ ] VS Code or Visual Studio 2022 installed
- [ ] GitHub Copilot extension enabled and signed in (inline suggestions working)
- [ ] `dotnet ef` v8.0.10 installed globally
- [ ] Repository cloned successfully
- [ ] `dotnet build` succeeds in `demo-project/`
- [ ] `dotnet test` passes (all tests green)
- [ ] Docker Desktop installed (if using SQL Server)
- [ ] `docker compose up -d` works and SQL container running (optional)
- [ ] Can open solution `demo-project/PortPulse.sln` without errors

## 7. Troubleshooting

| Issue | Solution |
|-------|----------|
| Copilot not suggesting | Check sign-in, license, extension enabled, network connectivity |
| dotnet-ef not found | Add global tools to PATH or reinstall with correct version |
| Build failures | Run `dotnet restore`, check SDK version, clear `bin/obj` if needed |
| Docker issues | Ensure Docker Desktop running, check port 1433 availability |
| SSL/Certificate errors | Trust dev cert: `dotnet dev-certs https --trust` |

## 8. Pre-Workshop Tips
- Familiarize yourself with the solution structure in `demo-project/`
- Review `docs/demo-marker-index.md` to understand markers
- Read `workshops/dotnet-maritime/01-foundations.md` for Session 1 overview
- Keep Copilot Chat open during exercises
- Always review generated code before accepting
