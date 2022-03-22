import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';
import { Field } from 'src/models/resource/field';

@Injectable({
  providedIn: 'root'
})
export class FieldsService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
              private http:HttpClient) { }

  getFields():Observable<Field[]> {
    return this.http.get<Field[]>(`${this.url}api/fields`);
  }

  addField(field:Field):Observable<number> {
    return this.http.post<number>(`${this.url}api/fields`, field);
  }

  editField(field:Field):Observable<any> {
    return this.http.put(`${this.url}api/fields`, field);
  }

  deleteField(field:Field):Observable<any> {
    return this.http.delete(`${this.url}api/fields/${field.id}`);
  }
}
