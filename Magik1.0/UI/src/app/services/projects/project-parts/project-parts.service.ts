import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Project} from "../../../models/project";
import {Observable} from "rxjs";
import {ProjectPart} from "../../../models/projectPart";
import {API_URL} from "../../../app-injection-tokens";

@Injectable({
  providedIn: 'root'
})
export class ProjectPartsService {

  constructor(private http: HttpClient,
              @Inject(API_URL) private url: string) { }

  getProjectPartsForProject(project: Project): Observable<Array<ProjectPart>> {
    return this.http.get<Array<ProjectPart>>(`${this.url}api/projectParts/forProject/` + project.id);
  }

  addProjectPart(projectPart: ProjectPart): Observable<ProjectPart> {
    return this.http.post<ProjectPart>(`${this.url}api/projectParts`, projectPart);
  }

  editProjectPart(projectPart: ProjectPart): Observable<any> {
    return this.http.put(`${this.url}api/projectParts/` + projectPart.id, projectPart);
  }

  deleteProjectPart(projectPart: ProjectPart): Observable<any> {
    return this.http.delete(`${this.url}api/projectParts/` + projectPart.id);
  }
}
