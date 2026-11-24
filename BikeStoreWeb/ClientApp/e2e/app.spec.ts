import { expect, test } from '@playwright/test';

test('home, navigation, counter and direct route reload work', async ({ page }) => {
  await page.goto('/');
  await expect(page.getByRole('heading', { name: 'Hello, world!' })).toBeVisible();
  await page.getByRole('link', { name: 'Counter', exact: true }).click();
  await expect(page.locator('strong')).toHaveText('0');
  await page.getByRole('button', { name: 'Increment' }).click();
  await expect(page.locator('strong')).toHaveText('1');
  await page.reload();
  await expect(page.getByRole('heading', { name: 'Counter', exact: true })).toBeVisible();
});

test('weather response renders after loading', async ({ page }) => {
  await page.route('**/weatherforecast', route => route.fulfill({ json: [
    { date: '2026-11-25', temperatureC: 20, temperatureF: 68, summary: 'Mild' }
  ] }));
  await page.goto('/fetch-data');
  await expect(page.getByRole('cell', { name: 'Mild' })).toBeVisible();
});

test('weather errors replace the loading message', async ({ page }) => {
  await page.route('**/weatherforecast', route => route.fulfill({ status: 503, body: '' }));
  await page.goto('/fetch-data');
  await expect(page.getByRole('alert')).toContainText('Unable to load');
  await expect(page.getByText('Loading...', { exact: true })).toHaveCount(0);
});

test('mobile menu opens and follows links', async ({ page }) => {
  await page.setViewportSize({ width: 390, height: 844 });
  await page.goto('/');
  const toggle = page.getByRole('button', { name: 'Toggle navigation' });
  await expect(toggle).toHaveAttribute('aria-expanded', 'false');
  await toggle.click();
  await expect(toggle).toHaveAttribute('aria-expanded', 'true');
  await page.getByRole('link', { name: 'Counter', exact: true }).click();
  await expect(page.getByRole('heading', { name: 'Counter', exact: true })).toBeVisible();
});
