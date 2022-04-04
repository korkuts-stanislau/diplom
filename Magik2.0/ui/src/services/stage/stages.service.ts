import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';
import { Project } from 'src/models/resource/project';
import { Stage } from 'src/models/resource/stage';

@Injectable({
  providedIn: 'root'
})
export class StagesService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
              private http:HttpClient) { }

  getStages(project:Project): Observable<Stage[]> {
    return this.http.get<Stage[]>(`${this.url}api/stages/${project.id}`);
  }

  addStage(project:Project, stage:Stage): Observable<Stage> {
    return this.http.post<Stage>(`${this.url}api/stages/${project.id}`, stage);
  }

  editStage(stage:Stage):Observable<Stage> {
    return this.http.put<Stage>(`${this.url}api/stages/${stage.id}`, stage);
  }

  deleteStage(stage:Stage): Observable<any> {
    return this.http.delete<any>(`${this.url}api/stages/${stage.id}`);
  }
}
