import {Inject, Injectable} from '@angular/core';
import {Observable, tap} from "rxjs";
import {Token} from "../../models/auth/token";
import {HttpClient} from "@angular/common/http";
import {API_URL} from "../../app-injection-tokens";
import {JwtHelperService} from "@auth0/angular-jwt";
import {SignIn} from "../../models/auth/signIn";
import {SignUp} from "../../models/auth/signUp";

export const ACCESS_TOKEN_KEY = "access_token"

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private url: string,
    private jwtHelper: JwtHelperService
  ) { }

  signin(signIn: SignIn): Observable<Token> {
    return this.http.post<Token>(`${this.url}api/auth/signin`, signIn).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
      })
    );
  }

  signup(signUp: SignUp): Observable<string> {
    return this.http.post<string>(`${this.url}api/auth/signup`, signUp);
  }

  isAuthenticated(): boolean {
    let token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  signout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
  }
}
