import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ProjectArea} from "../../models/projectArea";
import {API_URL} from "../../app-injection-tokens";
import {ProjectAreaIcon} from "../../models/icons/projectAreaIcon";

@Injectable({
  providedIn: 'root'
})
export class ProjectAreasService {

  constructor(private http: HttpClient,
              @Inject(API_URL) private url: string) { }

  getProjectAreaIcons(): Observable<Array<ProjectAreaIcon>> {
    return this.http.get<Array<ProjectAreaIcon>>("assets/projectAreaPictures.json");
  }

  getProjectAreas() : Observable<Array<ProjectArea>> {
    return this.http.get<Array<ProjectArea>>(`${this.url}api/projectAreas`);
  }

  createProjectArea(projectArea: ProjectArea) : Observable<ProjectArea> {
    return this.http.post<ProjectArea>(`${this.url}api/projectAreas`, projectArea);
  }

  editProjectArea(projectArea: ProjectArea) {
    return this.http.put(`${this.url}api/projectAreas/${projectArea.id}`, projectArea);
  }

  deleteProjectArea(projectArea: ProjectArea) {
    return this.http.delete(`${this.url}api/projectAreas/${projectArea.id}`);
  }
}
