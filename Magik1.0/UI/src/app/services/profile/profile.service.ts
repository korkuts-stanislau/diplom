import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {API_URL} from "../../app-injection-tokens";
import {Profile} from "../../models/profile";
import {Observable} from "rxjs";
import {ProfilePhoto} from "../../models/profile/profilePhoto";

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(private http: HttpClient,
              @Inject(API_URL) private url: string) { }

  getProfile(): Observable<Profile> {
    return this.http.get<Profile>(`${this.url}api/profile`);
  }

  changeProfilePhoto(base64Photo: string): Observable<ProfilePhoto> {
    return this.http.put<ProfilePhoto>(`${this.url}api/profile/changeProfilePhoto`, {
      base64Photo
    });
  }

  changeProfileName(newProfileName: string): Observable<any> {
    return this.http.put(`${this.url}api/profile/changeProfileName`, {
      newProfileName
    });
  }
}
