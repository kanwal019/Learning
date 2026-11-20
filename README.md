# Learning (Repository)

Overview
- Collection of sample projects, services and libraries used for learning .NET concepts, patterns and architectures.
- Includes web APIs, console apps, connectors, shared libraries and examples (e.g., OopsConsole, FrameworkLibrary, DataService, GatewayAPI, IdentityService).

Key points
- All 15 managed projects target .NET 11, including the shared libraries and FrameworkLibrary native wrapper.
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
- .NET 11 SDK 11.0.100 or a later stable 11.0 SDK, as configured in global.json
- Visual Studio with .NET 11 support, or VS Code with the C# extension
- To rebuild ClassicLibrary: Visual Studio C++ build tools with MSVC v143/v145 and Windows SDK 10.0.26100.0
- Node.js and npm for BikeStoreWeb's Angular frontend

Build (Windows PowerShell)
- Open PowerShell at repo root:
  ```powershell
  Set-Location W:\Repo\Learning
  dotnet restore Learning.sln
  dotnet build Learning.sln -c Release
  ```
- The default Any CPU build builds the managed projects and copies the bundled ClassicLibrary.dll. To also rebuild the native C++ project, run this in a Visual Studio Developer PowerShell:
  ```powershell
  msbuild Learning.sln /restore /p:Configuration=Release /p:Platform=x64
  ```
- FrameworkLibrary retains its assembly metadata and copies ClassicLibrary.dll into build and publish output. Its native examples require Windows and a matching process architecture.

Run & smoke test
- Start services you need (DataService, IdentityService, GatewayAPI, NetCoreApi) via their project folders:
  ```powershell
  Set-Location W:\Repo\Learning\DataService
  dotnet run
  ```
- Verify the configured endpoints with curl or a browser (DataService: /swagger in Development; NetCoreApi: /).
- RabbitMQ services require a running broker; database examples require SQL Server and their configured databases.

Tests
- No new automated tests detected in the latest branch. Add/enable unit tests under respective projects as required:
  ```powershell
  dotnet test
  ```

Migration notes
- Ensure CI/build agents and developers use the .NET 11 SDK selected by global.json.
- Entity Framework Core, ASP.NET Core authentication, JSON integration, and SPA packages use version 11.0.0. CommonLibrary uses the ASP.NET Core shared framework in place of its legacy ASP.NET Core 2.x packages.
- Swashbuckle and Newtonsoft.Json have been updated to avoid vulnerable transitive dependencies found during restore.
- Dockerfiles use the .NET 11 SDK/runtime images, copy referenced project files before restore, and explicitly retain HTTP port 80.
- Review the [.NET 11 compatibility notes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/11) when extending the samples. The legacy WebHost API still compiles but emits deprecation warnings.
- No DB schema changes detected in current changes; check individual services for migration steps.

Contributing
- Create feature branches from main.
- Follow repository coding/convention guidelines and update tests for refactors.
- Provide a concise PR description and run full build locally before opening a PR.

License
- Check repository root or project files for license information.

Contact
- Use project maintainers listed in repository or open an issue for questions.
