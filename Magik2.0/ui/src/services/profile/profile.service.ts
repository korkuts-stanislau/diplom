import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RESOURCE_API_URL} from "../../app/config/app-injection-tokens";
import {Observable} from "rxjs";
import {Profile} from "../../models/profile/profile";

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(private http: HttpClient,
              @Inject(RESOURCE_API_URL) private url: string) { }

  getProfile(): Observable<Profile> {
    return this.http.get<Profile>(`${this.url}api/profile/`);
  }

  editProfile(profile: Profile, isPictureEdited: boolean): Observable<any> {
    if(!isPictureEdited) {
      profile = {...profile};
      profile.picture = "";
    }
    return this.http.post(`${this.url}api/profile/`, profile);
  }

  validateProfile(profile?: Profile): string | undefined {
    if(!profile) return "Профиль пользователя должен присутствовать";
    if(profile.username.length > 50 ) return "Максимальная длина имени 50 символов";
    if(profile.description.length > 200 ) return "Максимальная длина описания 200 символов";
    return undefined;
  }
}
