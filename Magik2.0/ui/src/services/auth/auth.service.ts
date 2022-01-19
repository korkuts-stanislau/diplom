import {Inject, Injectable} from '@angular/core';
import {Auth} from "../../models/auth/auth";
import {Observable} from "rxjs";
import {tap} from "rxjs/operators";
import {Token} from "../../models/auth/token";
import {HttpClient} from "@angular/common/http";
import {
  ACCESS_TOKEN_KEY,
  AUTH_API_URL,
  tokenGetter,
  tokenKiller,
  tokenSetter
} from "../../app/config/app-injection-tokens";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient,
              @Inject(AUTH_API_URL) private url: string,
              private jwtHelper: JwtHelperService) {}

  public isAuthenticated(): boolean {
    let token = tokenGetter();
    // @ts-ignore
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  public signIn(auth:Auth): Observable<Token> {
    return this.http.post<Token>(`${this.url}api/auth/signIn`, auth)
      .pipe(
        tap(token => {
          tokenSetter(token.token);
        })
      )
  }

  public signUp(auth:Auth): Observable<Token> {
    return this.http.post<Token>(`${this.url}api/auth/signUp`, auth)
      .pipe(
        tap(token => {
          tokenSetter(token.token);
        })
      )
  }

  public signOut() {
    tokenKiller();
  }
}
