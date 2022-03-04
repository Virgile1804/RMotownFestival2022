import { Injectable } from '@angular/core';
import { Schedule } from '../api/models/schedule.model';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Artist } from '../api/models/artist.model';

@Injectable({
  providedIn: 'root'
})
export class FestivalApiService {
  private baseUrl = environment.apiBaseUrl + 'festival';

  constructor(private httpClient: HttpClient) { }

  getSchedule(): Observable<Schedule> {
    return this.httpClient.get<Schedule>(`${this.baseUrl}/lineup`);
  }

  getArtists(): Observable<Artist[]> {
    return this.httpClient.get<Artist[]>(`${this.baseUrl}/artists`);
  }
}
