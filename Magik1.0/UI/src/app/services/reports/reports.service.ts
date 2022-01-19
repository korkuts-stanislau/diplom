import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {API_URL} from "../../app-injection-tokens";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(@Inject(API_URL) private url: string,
              private http: HttpClient) { }

  getFullProjectsReport() : Observable<string> {
    return this.http.get(`${this.url}api/reports/projects`, {responseType: 'text'});
  }
}
