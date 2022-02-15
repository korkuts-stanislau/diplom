import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';
import { ProjectArea } from 'src/models/projects/projectArea';

@Injectable({
  providedIn: 'root'
})
export class ProjectAreaService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
              private http:HttpClient) { }

  getProjectAreas():Observable<ProjectArea[]> {
    return this.http.get<ProjectArea[]>(`${this.url}api/projectArea`);
  }

  addProjectArea(area:ProjectArea):Observable<number> {
    return this.http.post<number>(`${this.url}api/projectArea`, area);
  }

  editProjectArea(area:ProjectArea):Observable<any> {
    return this.http.put(`${this.url}api/projectArea`, area);
  }

  deleteProjectArea(area:ProjectArea):Observable<any> {
    return this.http.delete(`${this.url}api/projectArea/${area.id}`);
  }
}
