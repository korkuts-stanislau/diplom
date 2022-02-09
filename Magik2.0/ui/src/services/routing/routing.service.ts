import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoutingService {
  private currentPage:string = "projects";

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

  isHomepage():boolean {
    return this.currentPage == "homepage";
  }

  isProjects():boolean {
    return this.currentPage == "projects";
  }

  isProfile():boolean {
    return this.currentPage == "profile";
  }
}
