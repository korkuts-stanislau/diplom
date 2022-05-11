import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RESOURCE_API_URL} from "../../app/config/app-injection-tokens";
import {Observable} from "rxjs";
import {Profile} from "../../models/resource/profile";

@Injectable({
  providedIn: 'root'
})
export class ProfilesService {
  constructor(private http: HttpClient,
              @Inject(RESOURCE_API_URL) private url: string) { }

  getProfile(): Observable<Profile> {
    return this.http.get<Profile>(`${this.url}api/profiles/`);
  }

  editProfile(profile: Profile, isPictureEdited: boolean): Observable<any> {
    if(!isPictureEdited) {
      profile = {...profile};
      profile.picture = "";
    }
    return this.http.put(`${this.url}api/profiles/`, profile);
  }

  validateProfile(profile?: Profile): string | undefined {
    if(!profile) return "Профиль пользователя должен присутствовать";
    if(profile.username.length > 50 ) return "Максимальная длина имени 50 символов";
    if(profile.description.length > 200 ) return "Максимальная длина описания 200 символов";
    return undefined;
  }

  getAcceptedContacts(): Observable<Profile[]> {
    return this.http.get<Profile[]>(`${this.url}api/profiles/contacts/accepted`);
  }

  getRequestedContacts(): Observable<Profile[]> {
    return this.http.get<Profile[]>(`${this.url}api/profiles/contacts/requested`);
  }

  searchProfilesByName(name: string): Observable<Profile[]> {
    return this.http.get<Profile[]>(`${this.url}api/profiles/contacts/search?name=${name}`);
  }

  searchProfilesByDescription(description: string): Observable<Profile[]> {
    return this.http.get<Profile[]>(`${this.url}api/profiles/contacts/search?description=${description}`);
  }

  getOtherProfile(profile: Profile) {
    return this.http.get<Profile>(`${this.url}api/profiles/contacts/profile/${profile.id}`);
  }

  deleteContact(profile: Profile): Observable<any> {
    return this.http.delete<any>(`${this.url}api/profiles/contacts/${profile.id}`);
  }

  declineRequest(profile: Profile): Observable<any> {
    return this.http.delete<any>(`${this.url}api/profiles/contacts/requests/${profile.id}`);
  }

  acceptContact(profile: Profile): Observable<any> {
    return this.http.post<any>(`${this.url}api/profiles/contacts/requests/accept`, profile.id);
  }

  sendRequestToProfile(profile: Profile): Observable<any> {
    return this.http.post<any>(`${this.url}api/profiles/contacts/requests/send`, profile.id);
  }
}
