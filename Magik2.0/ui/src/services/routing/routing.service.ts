import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppRoutingService {
  private currentPage:string = "chat";

  constructor() {}

  routeToHomepage() {
    this.currentPage = "homepage";
  }

  routeToProjects() {
    this.currentPage = "projects";
  }

  routeToProfile() {
    this.currentPage = "profile";
  }

  routeToChat() {
    this.currentPage = "chat";
  }

  isHomepage():boolean {
    return this.currentPage == "homepage";
  }

  isProjects():boolean {
    return this.currentPage == "projects";
  }

  isProfile():boolean {
    return this.currentPage == "profile";
  }

  isChat():boolean {
    return this.currentPage == "chat";
  }
}
