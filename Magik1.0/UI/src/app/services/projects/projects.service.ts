import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Project} from "../../models/project";
import {API_URL} from "../../app-injection-tokens";
import {ProjectArea} from "../../models/projectArea";

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  constructor(@Inject(API_URL) private url: string,
              private http: HttpClient) { }

  getProjectsForProjectArea(area: ProjectArea): Observable<Array<Project>> {
    return this.http.get<Array<Project>>(`${this.url}api/projects/forArea/` + area.id);
  }

  createProject(project: Project): Observable<Project> {
    return this.http.post<Project>(`${this.url}api/projects`, project);
  }

  editProject(project: Project): Observable<any> {
    return this.http.put(`${this.url}api/projects/` + project.id, project);
  }

  deleteProject(project: Project): Observable<any> {
    return this.http.delete(`${this.url}api/projects/` + project.id);
  }

  getProjectProgress(project: Project): Observable<number> {
    return this.http.get<number>(`${this.url}api/projects/progress/` + project.id);
  }
}
