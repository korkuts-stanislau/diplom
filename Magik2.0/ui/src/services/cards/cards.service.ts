import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';
import { Card } from 'src/models/resource/card';
import { Project } from 'src/models/resource/project';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
    private http:HttpClient) { }

    getCards(project: Project): Observable<Card[]> {
      return this.http.get<Card[]>(`${this.url}api/cards/${project.id}`);
    }
  
    addCard(project:Project, card:Card): Observable<Card> {
      return this.http.post<Card>(`${this.url}api/cards/${project.id}`, card);
    }
  
    editCard(card:Card):Observable<Card> {
      return this.http.put<Card>(`${this.url}api/cards/${card.id}`, card);
    }
  
    deleteCard(card:Card): Observable<any> {
      return this.http.delete<any>(`${this.url}api/cards/${card.id}`);
    }
}
