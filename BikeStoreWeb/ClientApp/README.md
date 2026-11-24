# BikeStoreWeb client

Angular 22.3, TypeScript 6, Bootstrap 5 and RxJS 7. The app uses standalone components and Angular's default zoneless change detection. Home, Counter and Fetch data retain their existing routes.

## Prerequisites

Use Node.js 22.22.3+, 24.15.0+, or 26+ (within the ranges in package.json), and npm 10+. Node 24 LTS is recommended. The backend requires the repository's .NET 11 SDK.

## Development

From this directory:

```powershell
npm ci
npm start
```

In another terminal, from BikeStoreWeb:

```powershell
dotnet run
```

Open https://localhost:5001 for the complete app. ASP.NET Core proxies page requests to Angular at http://127.0.0.1:4200; start the client separately before browsing. You can also open Angular directly: proxy.conf.json forwards /weatherforecast to https://localhost:5001. Adjust that target if you change the backend launch URL. The development proxy accepts the local development certificate; production API requests use the application's own origin.

## Build and publish

```powershell
npm run build
```

The production build writes index.html and assets directly into dist, matching ASP.NET Core's ClientApp/dist static-file root. From the repository root:

```powershell
dotnet publish BikeStoreWeb/BikeStoreWeb.csproj -c Release
```

Publishing runs npm ci and the production Angular build and includes dist in the output. The unused Angular Universal/prerendering configuration has been removed; ASP.NET Core serves the client and handles API requests.

## Tests

```powershell
npm test -- --watch=false
npx playwright install chromium
npm run e2e
```

Unit tests use Vitest and jsdom. Playwright starts the Angular development server and checks navigation, counter interaction, mobile navigation, and weather success/failure rendering. Weather responses are intercepted, so these checks do not require the backend.

To run the same browser checks against a running published app:

```powershell
$env:E2E_BASE_URL = 'http://127.0.0.1:5080'
npm run e2e
```

The old Karma, Protractor, TSLint, node-sass, jQuery and Angular Universal dependencies are no longer used. Navigation collapse is controlled by Angular and does not require Bootstrap JavaScript.
