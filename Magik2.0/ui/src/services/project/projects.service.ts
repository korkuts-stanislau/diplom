import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';
import { Field } from 'src/models/resource/field';
import { Project } from 'src/models/resource/project';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
    private http:HttpClient) { }

  getProjects(field: Field): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.url}api/projects/${field.id}`);
  }

  getProject(projectId: number): Observable<Project> {
    return this.http.get<Project>(`${this.url}api/projects/single/${projectId}`);
  }

  addProject(field: Field, project:Project): Observable<Project> {
    return this.http.post<Project>(`${this.url}api/projects/${field.id}`, project);
  }

  editProject(project:Project):Observable<Project> {
    return this.http.put<Project>(`${this.url}api/projects/${project.id}`, project);
  }

  deleteProject(project: Project): Observable<any> {
    return this.http.delete<any>(`${this.url}api/projects/${project.id}`);
  }
}
