import {InjectionToken} from "@angular/core";

export const AUTH_API_URL = new InjectionToken<string>("auth api url");
export const RESOURCE_API_URL = new InjectionToken<string>("resource api url");
export const CHAT_API_URL = new InjectionToken<string>("chat api url");

export const ACCESS_TOKEN_KEY = "access_token";

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

export function tokenSetter(token: string) {
  return localStorage.setItem(ACCESS_TOKEN_KEY, token);
}

export function tokenKiller() {
  localStorage.removeItem(ACCESS_TOKEN_KEY);
}
