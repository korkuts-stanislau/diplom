import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';

@Injectable({
  providedIn: 'root'
})
export class AttachmentsService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
    private http:HttpClient) { }

  
}
