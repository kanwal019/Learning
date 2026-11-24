import { Component, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  readonly forecasts = signal<WeatherForecast[] | null>(null);
  readonly error = signal(false);

  constructor() {
    inject(HttpClient).get<WeatherForecast[]>('weatherforecast').subscribe({
      next: forecasts => this.forecasts.set(forecasts),
      error: () => this.error.set(true)
    });
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
