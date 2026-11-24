import { defineConfig } from '@playwright/test';

export default defineConfig({
  testDir: './e2e',
  use: { baseURL: process.env['E2E_BASE_URL'] || 'http://127.0.0.1:4200' },
  webServer: process.env['E2E_BASE_URL'] ? undefined : {
    command: 'npm start',
    url: 'http://127.0.0.1:4200',
    reuseExistingServer: !process.env['CI']
  }
});
