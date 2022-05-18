import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';
import { Statistic } from 'src/models/resource/statistic';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
              private http:HttpClient) { }
  
  get(): Observable<Statistic> {
    return this.http.get<Statistic>(`${this.url}api/statistic`);
  }
}
