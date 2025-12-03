# Learning (Repository)

Overview
- Collection of sample projects, services and libraries used for learning .NET concepts, patterns and architectures.
- Includes web APIs, console apps, connectors, shared libraries and examples (e.g., OopsConsole, FrameworkLibrary, DataService, GatewayAPI, IdentityService).

Key points
- All projects have been upgraded to .NET 10.
- ClassicLibrary bumped to v145.
- Several modules refactored for clarity and maintainability.

Repository layout (high level)
- ActivityService/            — Background/service project
- DataService/                — Data API and startup code
- GatewayAPI/                 — API gateway and controllers
- IdentityService/            — Authentication/identity service
- NetCoreApi/, BikeStoreWeb/  — Example APIs / web apps
- FrameworkLibrary/, Common/  — Shared libraries and helpers
- OopsConsole/                — Console examples (delegates, LINQ, generics, etc.)
- ClassicLibrary/             — Native/classic project (vcxproj)
- SchoolConnector/, BikeStoresConnector/ — Example connectors
- .vscode/                    — Editor settings

Prerequisites
- Windows 10/11
- .NET 10 SDK (install matching runtime for CI/build agents)
- Optional: Visual Studio 2022/2025 or VS Code with C# extension

Build (Windows PowerShell)
- Open PowerShell at repo root:
  ```powershell
  cd /d W:\Repo\Learning
  dotnet restore
  dotnet build -c Release
  ```

Run & smoke test
- Start services you need (DataService, IdentityService, GatewayAPI, NetCoreApi) via their project folders:
  ```powershell
  cd /d W:\Repo\Learning\DataService
  dotnet run
  ```
- Verify endpoints with curl or a browser (e.g., http://localhost:{port}/health or /swagger).

Tests
- No new automated tests detected in the latest branch. Add/enable unit tests under respective projects as required:
  ```powershell
  dotnet test
  ```

Migration notes
- Ensure CI/build agents and developers use .NET 10 SDK.
- No DB schema changes detected in current changes; check individual services for migration steps.

Contributing
- Create feature branches from main.
- Follow repository coding/convention guidelines and update tests for refactors.
- Provide a concise PR description and run full build locally before opening a PR.

License
- Check repository root or project files for license information.

Contact
- Use project maintainers listed in repository or open an issue for questions.